namespace Fashionista.Application.ShoppingCart.Commands.AddSesssion
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using MediatR;

    public class AddSessionProductInCartCommand : IRequest<List<CartProductLookupModel>>
    {
        public List<CartProductLookupModel> Session { get; set; }

        public int Id { get; set; }

        public int? Quantity { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }
    }
}
