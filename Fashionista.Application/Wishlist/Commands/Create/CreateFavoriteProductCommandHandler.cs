namespace Fashionista.Application.Wishlist.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CreateFavoriteProductCommandHandler : IRequestHandler<CreateFavoriteProductCommand, int>
    {
        private readonly IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IUserAssistant userAssistant;

        public CreateFavoriteProductCommandHandler(
            IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository,
            IDeletableEntityRepository<Product> productsRepository,
            IUserAssistant userAssistant)
        {
            this.favoriteProductsRepository = favoriteProductsRepository;
            this.productsRepository = productsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(CreateFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await CommonCheckAssistant.CheckIfProductExists(request.Id, this.productsRepository))
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            if (await this.CheckIfUserContainsFavoriteProductAlready(request.Id, this.userAssistant.UserId))
            {
                throw new EntityAlreadyExistsException(nameof(request), request.Id);
            }

            var favoriteProduct = new FavoriteProduct
            {
                ApplicationUserId = this.userAssistant.UserId,
                ProductId = request.Id,
            };

            await this.favoriteProductsRepository.AddAsync(favoriteProduct);
            await this.favoriteProductsRepository.SaveChangesAsync(cancellationToken);

            return favoriteProduct.ProductId;
        }

        private async Task<bool> CheckIfUserContainsFavoriteProductAlready(int id, string userId) =>
            await this.favoriteProductsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.ProductId == id && x.ApplicationUserId == userId);
    }
}
