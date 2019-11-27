namespace Fashionista.Application.Products.Queries.GetAllMenProducts
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllMenProductsViewModel
    {
        public IEnumerable<ProductLookupModel> Products { get; set; }
    }
}
