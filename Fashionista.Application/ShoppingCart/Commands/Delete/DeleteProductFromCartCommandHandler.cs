namespace Fashionista.Application.ShoppingCart.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteProductFromCartCommandHandler : IRequestHandler<DeleteProductFromCartCommand, int>
    {
        private readonly IUserAssistant userAssistant;
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;

        public DeleteProductFromCartCommandHandler(
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IUserAssistant userAssistant)
        {
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var product = await this.shoppingCartProductsRepository
                              .All()
                              .SingleOrDefaultAsync(
                                  x => x.ProductId == request.Id
                                       && x.ShoppingCartId == this.userAssistant.ShoppingCartId, cancellationToken)
                          ?? throw new NotFoundException(nameof(ShoppingCartProduct), request.Id);

            this.shoppingCartProductsRepository.HardDelete(product);
            await this.shoppingCartProductsRepository.SaveChangesAsync(cancellationToken);

            return product.ProductId;
        }
    }
}
