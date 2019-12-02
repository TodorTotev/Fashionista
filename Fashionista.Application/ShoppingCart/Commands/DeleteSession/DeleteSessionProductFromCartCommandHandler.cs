#pragma warning disable 1998
namespace Fashionista.Application.ShoppingCart.Commands.DeleteSession
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
        DeleteSessionProductFromCartCommandHandler : IRequestHandler<DeleteSessionProductFromCartCommand,
            List<CartProductLookupModel>>
    {
        private readonly IShoppingCartAssistant shoppingCartAssistant;

        public DeleteSessionProductFromCartCommandHandler(IShoppingCartAssistant shoppingCartAssistant)
        {
            this.shoppingCartAssistant = shoppingCartAssistant;
        }

        public async Task<List<CartProductLookupModel>> Handle(
            DeleteSessionProductFromCartCommand request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var product = request.Session.SingleOrDefault(x => x.ProductId == request.Id)
                          ?? throw new NotFoundException(nameof(ShoppingCartProduct), request.Id);

            request.Session.Remove(product);

            this.shoppingCartAssistant.Set(request.Session);
            return request.Session;
        }
    }
}
