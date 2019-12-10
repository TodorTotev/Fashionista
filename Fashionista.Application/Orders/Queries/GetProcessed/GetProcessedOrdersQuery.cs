namespace Fashionista.Application.Orders.Queries.GetProcessed
{
    using MediatR;

    public class GetProcessedOrdersQuery : IRequest<OrdersViewModel>
    {
        public bool IsDelivered { get; set; }
    }
}
