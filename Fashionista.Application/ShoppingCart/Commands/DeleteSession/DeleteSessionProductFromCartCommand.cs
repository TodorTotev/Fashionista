namespace Fashionista.Application.ShoppingCart.Commands.DeleteSession
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class DeleteSessionProductFromCartCommand : IRequest<List<CartProductLookupModel>>
    {
        public int Id { get; set; }

        public List<CartProductLookupModel> Session { get; set; }
    }
}
