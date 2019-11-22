namespace Fashionista.Application.Common.Models
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class ProductAttributesLookupModel : IMapFrom<ProductAttributes>
    {
        public string Color { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }
    }
}