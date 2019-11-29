namespace Fashionista.Application.Common.Models
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class AddressLookupModel : IMapFrom<Address>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CityName { get; set; }

        public string CityPostcode { get; set; }
    }
}
