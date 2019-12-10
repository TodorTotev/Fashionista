namespace Fashionista.Application.Orders.Queries.GetProcessed
{
    using MediatR;

    public class GetProcessedOrdersPagedQuery : IRequest<OrdersViewModel>
    {
        public bool IsDelivered { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
