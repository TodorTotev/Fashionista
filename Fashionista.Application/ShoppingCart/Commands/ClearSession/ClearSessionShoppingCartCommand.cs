namespace Fashionista.Application.ShoppingCart.Commands.ClearSession
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class ClearSessionShoppingCartCommand : IRequest<List<CartProductLookupModel>>
    {
        public List<CartProductLookupModel> Session { get; set; }
    }
}
