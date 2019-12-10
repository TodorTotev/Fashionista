namespace Fashionista.Application.Common.Models
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class ProductSizeLookupModel : IMapFrom<ProductSize>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
