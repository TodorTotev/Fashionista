namespace Fashionista.Application.ShoppingCart.Commands.Add
{
    using MediatR;

    public class AddProductInCartCommand : IRequest<int>
    {
        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }
    }
}
