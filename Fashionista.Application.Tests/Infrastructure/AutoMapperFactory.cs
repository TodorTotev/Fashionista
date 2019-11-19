namespace Fashionista.Application.Tests.Infrastructure
{
    using Fashionista.Application.Infrastructure.Automapper;
    using AutoMapper;

    public class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mappingConfig.CreateMapper();
        }
    }
}