namespace Fashionista.Application.Wishlist.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteFavoriteProductCommandHandler : IRequestHandler<DeleteFavoriteProductCommand, int>
    {
        private readonly IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository;

        public DeleteFavoriteProductCommandHandler(
            IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository)
        {
            this.favoriteProductsRepository = favoriteProductsRepository;
        }

        public async Task<int> Handle(DeleteFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.favoriteProductsRepository
                                        .AllWithDeleted()
                                        .SingleOrDefaultAsync(
                                            x => x.Product.Id == request.Id
                                                 && x.ApplicationUserId == request.User.Id,
                                            cancellationToken)
                                    ?? throw new NotFoundException(nameof(FavoriteProduct), request.Id);

            this.favoriteProductsRepository.Delete(requestedEntity);
            await this.favoriteProductsRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.ProductId;
        }
    }
}
