namespace Fashionista.Application.ShoppingCart.Commands.AddSesssion
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class AddSessionProductCartCommand : IRequest<List<CartProductLookupModel>>
    {
        public List<CartProductLookupModel> Session { get; set; }

        public int Id { get; set; }
    }
}
