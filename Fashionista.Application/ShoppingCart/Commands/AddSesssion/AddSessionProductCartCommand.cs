using System.Collections.Generic;
using Fashionista.Application.Common.Models;

namespace Fashionista.Application.ShoppingCart.Commands.AddSesssion
{
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using MediatR;

    public class AddSessionProductCartCommand : IRequest<List<CartProductLookupModel>>
    {
        public int Id { get; set; }
    }
}
