namespace Fashionista.Application.Common.Models
{
    using System.Collections.Generic;

    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;

    public class OrderProductLookupModel : IMapFrom<ShoppingCartProduct>, IMapFrom<OrderProduct>
    {
        public int ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }
    }
}
