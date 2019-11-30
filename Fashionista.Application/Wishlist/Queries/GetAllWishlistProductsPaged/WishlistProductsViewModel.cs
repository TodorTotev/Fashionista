namespace Fashionista.Application.Wishlist.Queries.GetAllWishlistProducts
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class WishlistProductsViewModel
    {
        public IEnumerable<WishlistProductLookup> Products { get; set; }
    }
}