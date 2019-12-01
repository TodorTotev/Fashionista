namespace Fashionista.Application.ShoppingCart.Commands.Add
{
    using MediatR;

    public class AddProductInCartCommand : IRequest<int>
    {
        public int ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}
