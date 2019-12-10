namespace Fashionista.Application.Orders.Queries.GetProcessed
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class OrdersViewModel
    {
        public IEnumerable<OrderLookupModel> CurrentOrders { get; set; }
    }
}
