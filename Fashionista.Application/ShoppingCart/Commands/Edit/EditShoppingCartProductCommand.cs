namespace Fashionista.Application.ShoppingCart.Commands.Edit
{
    using MediatR;

    public class EditShoppingCartProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }
}
