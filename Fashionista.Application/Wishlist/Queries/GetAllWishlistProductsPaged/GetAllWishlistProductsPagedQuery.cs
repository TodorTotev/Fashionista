namespace Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged
{
    using Fashionista.Application.Wishlist.Queries.GetAllWishlistProducts;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetAllWishlistProductsPagedQuery : IRequest<WishlistProductsViewModel>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ApplicationUser User { get; set; }
    }
}
