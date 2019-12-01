namespace Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        GetAllWishlistProductsPagedQueryHandler : IRequestHandler<GetAllWishlistProductsPagedQuery,
            WishlistProductsViewModel>
    {
        private readonly IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository;
        private readonly IMapper mapper;

        public GetAllWishlistProductsPagedQueryHandler(
            IDeletableEntityRepository<FavoriteProduct> favoriteProductsRepository,
            IMapper mapper)
        {
            this.favoriteProductsRepository = favoriteProductsRepository;
            this.mapper = mapper;
        }

        public async Task<WishlistProductsViewModel> Handle(
            GetAllWishlistProductsPagedQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var products = await this.favoriteProductsRepository
                .All()
                .Where(x => x.ApplicationUserId == request.User.Id)
                .Select(x => x.Product)
                .Skip(request.PageNumber * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProductLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new WishlistProductsViewModel
            {
                Products = products,
            };
        }
    }
}
