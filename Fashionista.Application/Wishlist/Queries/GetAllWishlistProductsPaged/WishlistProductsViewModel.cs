namespace Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class WishlistProductsViewModel
    {
        public IEnumerable<ProductLookupModel> Products { get; set; }
    }
}