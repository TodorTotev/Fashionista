#pragma warning disable 1998
namespace Fashionista.Application.ShoppingCart.Commands.ClearSession
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using MediatR;

    public class
        ClearSessionShoppingCartCommandHandler : IRequestHandler<ClearSessionShoppingCartCommand,
            List<CartProductLookupModel>>
    {
        private readonly IShoppingCartAssistant shoppingCartAssistant;

        public ClearSessionShoppingCartCommandHandler(IShoppingCartAssistant shoppingCartAssistant)
        {
            this.shoppingCartAssistant = shoppingCartAssistant;
        }

        public async Task<List<CartProductLookupModel>> Handle(
            ClearSessionShoppingCartCommand request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            request.Session.Clear();

            this.shoppingCartAssistant.Set(request.Session);

            return request.Session;
        }
    }
}
