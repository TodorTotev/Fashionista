namespace Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        GetAllShoppingCartProductsQueryHandler : IRequestHandler<GetAllShoppingCartProductsQuery,
            AllShoppingCartProductsViewModel>
    {
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IUserAssistant userAssistant;

        public GetAllShoppingCartProductsQueryHandler(
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IUserAssistant userAssistant)
        {
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<AllShoppingCartProductsViewModel> Handle(
            GetAllShoppingCartProductsQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            var products = await this.shoppingCartProductsRepository
                .All()
                .Where(x => x.ShoppingCartId == this.userAssistant.ShoppingCartId)
                .To<CartProductLookupModel>()
                .ToListAsync(cancellationToken);

            return new AllShoppingCartProductsViewModel
            {
                ShoppingCartProducts = products,
            };
        }
    }
}
