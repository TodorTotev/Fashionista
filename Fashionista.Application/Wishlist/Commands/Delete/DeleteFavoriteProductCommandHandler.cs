namespace Fashionista.Application.Wishlist.Commands.Delete
{
    using System;
    using System.Linq;
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
        private readonly IUserAssistant userAssistant;

        public DeleteFavoriteProductCommandHandler(
            IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository,
            IUserAssistant userAssistant)
        {
            this.favoriteProductsRepository = favoriteProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(DeleteFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var requestedEntity = await this.favoriteProductsRepository
                                        .AllWithDeleted()
                                        .Where(x => x.Product.Id == request.Id
                                                    && x.ApplicationUserId == this.userAssistant.UserId)
                                        .SingleOrDefaultAsync(cancellationToken)
                                    ?? throw new NotFoundException(nameof(FavoriteProduct), request.Id);

            this.favoriteProductsRepository.HardDelete(requestedEntity);
            await this.favoriteProductsRepository.SaveChangesAsync(cancellationToken);

            return requestedEntity.ProductId;
        }
    }
}
