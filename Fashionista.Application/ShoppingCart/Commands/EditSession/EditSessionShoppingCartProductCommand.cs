namespace Fashionista.Application.ShoppingCart.Commands.EditSession
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class EditSessionShoppingCartProductCommand : IRequest<List<CartProductLookupModel>>
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public List<CartProductLookupModel> Session { get; set; }
    }
}
