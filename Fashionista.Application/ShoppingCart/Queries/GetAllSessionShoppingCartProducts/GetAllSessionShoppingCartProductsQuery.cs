namespace Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetAllSessionShoppingCartProductsQuery : IRequest<List<CartProductLookupModel>>
    {
    }
}
