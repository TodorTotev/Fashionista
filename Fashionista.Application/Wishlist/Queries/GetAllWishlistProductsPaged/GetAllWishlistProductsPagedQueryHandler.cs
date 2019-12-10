namespace Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged
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
        GetAllWishlistProductsPagedQueryHandler : IRequestHandler<GetAllWishlistProductsPagedQuery,
            WishlistProductsViewModel>
    {
        private readonly IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository;
        private readonly IUserAssistant userAssistant;

        public GetAllWishlistProductsPagedQueryHandler(
            IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository,
            IUserAssistant userAssistant)
        {
            this.favoriteProductsRepository = favoriteProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<WishlistProductsViewModel> Handle(
            GetAllWishlistProductsPagedQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var products = await this.favoriteProductsRepository
                .All()
                .Where(x => x.ApplicationUserId == this.userAssistant.UserId)
                .Select(x => x.Product)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .To<ProductLookupModel>()
                .ToListAsync(cancellationToken);

            return new WishlistProductsViewModel
            {
                Products = products,
            };
        }
    }
}
