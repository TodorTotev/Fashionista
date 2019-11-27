namespace Fashionista.Application.Products.Queries.GetAllWomenProducts
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllWomenProductsViewModel
    {
        public IEnumerable<ProductLookupModel> Products { get; set; }
    }
}