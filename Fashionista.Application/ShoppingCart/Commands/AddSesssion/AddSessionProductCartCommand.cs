namespace Fashionista.Application.ShoppingCart.Commands.AddSesssion
{
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using MediatR;

    public class AddSessionProductCartCommand : IRequest<AllShoppingCartProductsViewModel>
    {
        public int Id { get; set; }
    }
}
