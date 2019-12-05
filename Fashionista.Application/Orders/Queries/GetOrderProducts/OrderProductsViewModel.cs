namespace Fashionista.Application.Orders.Queries.GetOrderProducts
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class OrderProductsViewModel
    {
        public IEnumerable<OrderProductLookupModel> Products { get; set; }
    }
}