namespace Fashionista.Infrastructure.Cloudinary
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.Configuration;

    public class CloudinaryFactory
    {
        public static CloudinaryDotNet.Cloudinary GetInstance(IConfiguration configuration)
        {
            var cloud = configuration["cloudinary-cloud"];
            var apiKey = configuration["cloudinary-apiKey"];
            var apiSecret = configuration["cloudinary-apiSecret"];

            var account = new Account(cloud, apiKey, apiSecret);

            var instance = new Cloudinary(account);
            return instance;
        }
    }
}
