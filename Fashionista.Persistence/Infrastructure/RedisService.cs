namespace Fashionista.Persistence.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Common;
    using Microsoft.Extensions.Options;
    using StackExchange.Redis;

    public class RedisService<T> : IRedisService<T>
    {
        public IDatabase Database { get; private set; }

        public RedisService(IOptions<RedisConfigurationOptions> redisConfig)
        {
            this.Database = ConnectionMultiplexer.Connect(redisConfig.Value.Host).GetDatabase();
        }

        public async Task Save(string key, T data, TimeSpan? expiration = null)
        {
            var serializedObject = JsonAssistant.SerializeObjectToJson(data);
            await this.Database.StringSetAsync(key, serializedObject, expiration);
        }

        public async Task<T> Get(string key)
        {
            try
            {
                var res = await this.Database.StringGetAsync(key);

                return res.IsNull ? default : JsonAssistant.DeserializeObjectFromJson<T>(res);
            }
            catch
            {
                return default;
            }
        }

        public async Task Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key.Contains(":"))
            {
                throw new ArgumentException("invalid key");
            }

            await this.Database.KeyDeleteAsync(key);
        }
    }
}
