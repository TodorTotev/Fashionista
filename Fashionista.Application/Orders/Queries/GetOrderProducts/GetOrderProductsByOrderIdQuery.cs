namespace Fashionista.Application.Orders.Queries.GetOrderProducts
{
    using MediatR;

    public class GetOrderProductsByOrderIdQuery : IRequest<OrderProductsViewModel>
    {
        public int Id { get; set; }
    }
}
