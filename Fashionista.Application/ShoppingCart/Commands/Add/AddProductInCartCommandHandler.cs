namespace Fashionista.Application.ShoppingCart.Commands.Add
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class AddProductInCartCommandHandler : IRequestHandler<AddProductInCartCommand, int>
    {
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IUserAssistant userAssistant;

        public AddProductInCartCommandHandler(
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IDeletableEntityRepository<Product> productsRepository,
            IUserAssistant userAssistant)
        {
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.productsRepository = productsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(AddProductInCartCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductExists(request.Id, this.productsRepository))
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            if (await this.CheckIfProductIsPresentInUserShoppingCart(request.Id, this.userAssistant.ShoppingCartId))
            {
                throw new EntityAlreadyExistsException(nameof(request), request.Id);
            }

            var product = new ShoppingCartProduct
            {
                ProductId = request.Id,
                ShoppingCartId = this.userAssistant.ShoppingCartId,
                Quantity = request.Quantity ?? 1,
            };

            await this.shoppingCartProductsRepository.AddAsync(product);
            await this.shoppingCartProductsRepository.SaveChangesAsync(cancellationToken);

            return product.ProductId;
        }

        private async Task<bool> CheckIfProductIsPresentInUserShoppingCart(int productId, int userShoppingCartId) =>
            await this.shoppingCartProductsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.ShoppingCartId == userShoppingCartId
                               && x.ProductId == productId);
    }
}
