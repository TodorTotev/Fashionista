namespace Fashionista.Application.ShoppingCart.Commands.Delete
{
    using MediatR;

    public class DeleteProductFromCartCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
