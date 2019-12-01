namespace Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged
{
    using MediatR;

    public class GetAllWishlistProductsPagedQuery : IRequest<WishlistProductsViewModel>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
