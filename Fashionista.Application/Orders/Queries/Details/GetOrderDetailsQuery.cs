namespace Fashionista.Application.Orders.Queries.Details
{
    using MediatR;

    public class GetOrderDetailsQuery : IRequest<OrderDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
