namespace Fashionista.Application.Products.Queries.GetAllProductsPaged
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllProductsPagedViewModel
    {
        public IEnumerable<ProductLookupModel> Products { get; set; }
    }
}