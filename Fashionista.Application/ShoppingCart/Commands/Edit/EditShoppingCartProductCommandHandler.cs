namespace Fashionista.Application.ShoppingCart.Commands.Edit
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

    public class EditShoppingCartProductCommandHandler : IRequestHandler<EditShoppingCartProductCommand, int>
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IUserAssistant userAssistant;

        public EditShoppingCartProductCommandHandler(
            IDeletableEntityRepository<Product> productsRepository,
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IUserAssistant userAssistant)
        {
            this.productsRepository = productsRepository;
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(EditShoppingCartProductCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductExists(request.Id, this.productsRepository))
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var shoppingCartProduct = await this.shoppingCartProductsRepository
                    .All()
                    .SingleOrDefaultAsync(
                        x => x.ProductId == request.Id
                        && x.ShoppingCartId == this.userAssistant.ShoppingCartId, cancellationToken)
                ?? throw new NotFoundException(nameof(ShoppingCartProduct), request.Id);

            if (request.Quantity <= 0)
            {
                return shoppingCartProduct.ProductId;
            }

            shoppingCartProduct.Quantity = request.Quantity;

            this.shoppingCartProductsRepository.Update(shoppingCartProduct);
            await this.shoppingCartProductsRepository.SaveChangesAsync(cancellationToken);

            return shoppingCartProduct.ProductId;
        }
    }
}
