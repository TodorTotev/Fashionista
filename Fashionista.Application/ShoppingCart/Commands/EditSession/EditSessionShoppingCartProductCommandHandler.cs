#pragma warning disable 1998
namespace Fashionista.Application.ShoppingCart.Commands.EditSession
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class
        EditSessionShoppingCartProductCommandHandler : IRequestHandler<EditSessionShoppingCartProductCommand,
            List<CartProductLookupModel>>
    {
        private readonly IShoppingCartAssistant shoppingCartAssistant;

        public EditSessionShoppingCartProductCommandHandler(IShoppingCartAssistant shoppingCartAssistant)
        {
            this.shoppingCartAssistant = shoppingCartAssistant;
        }

        public async Task<List<CartProductLookupModel>> Handle(
            EditSessionShoppingCartProductCommand request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var product = request.Session.SingleOrDefault(x => x.ProductId == request.Id)
                          ?? throw new NotFoundException(nameof(ShoppingCartProduct), request.Id);

            product.Quantity = request.Quantity;
            product.TotalPrice = request.Quantity * product.ProductPrice;

            this.shoppingCartAssistant.Set(request.Session);

            return request.Session;
        }
    }
}
