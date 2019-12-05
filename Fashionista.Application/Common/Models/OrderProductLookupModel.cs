namespace Fashionista.Application.Common.Models
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class OrderProductLookupModel : IMapFrom<ShoppingCartProduct>
    {
        public int ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
    }
}
