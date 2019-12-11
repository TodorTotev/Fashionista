namespace Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using MediatR;

    public class GetAllSessionShoppingCartProductsQueryHandler : IRequestHandler<GetAllSessionShoppingCartProductsQuery,
        List<CartProductLookupModel>>
    {
        private readonly IShoppingCartAssistant shoppingCartAssistant;

        public GetAllSessionShoppingCartProductsQueryHandler(IShoppingCartAssistant shoppingCartAssistant)
        {
            this.shoppingCartAssistant = shoppingCartAssistant;
        }

        public async Task<List<CartProductLookupModel>> Handle(
            GetAllSessionShoppingCartProductsQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var test = this.shoppingCartAssistant.Get();

            return this.shoppingCartAssistant.Get();
        }
    }
}