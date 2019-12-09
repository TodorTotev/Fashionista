namespace Fashionista.Application.Tests.Infrastructure
{
    using System.Reflection;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(AddressLookupModel).GetTypeInfo().Assembly);
        }
    }
}
