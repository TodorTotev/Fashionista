namespace Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class AllShoppingCartProductsViewModel
    {
        public IEnumerable<CartProductLookupModel> ShoppingCartProducts { get; set; }
    }
}
