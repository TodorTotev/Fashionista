namespace Fashionista.Application.ShoppingCart.Commands.Clear
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class ClearShoppingCartCommandHandler : IRequestHandler<ClearShoppingCartCommand, int>
    {
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IUserAssistant userAssistant;

        public ClearShoppingCartCommandHandler(
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IUserAssistant userAssistant)
        {
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(ClearShoppingCartCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var products = await this.shoppingCartProductsRepository
                                          .All()
                                          .Where(x => x.ShoppingCartId == this.userAssistant.ShoppingCartId)
                                          .ToListAsync(cancellationToken);

            products.ForEach(x => this.shoppingCartProductsRepository.Delete(x));

            return await this.shoppingCartProductsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
