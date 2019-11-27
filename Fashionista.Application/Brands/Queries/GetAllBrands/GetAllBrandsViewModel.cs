namespace Fashionista.Application.Brands.Queries.GetAllBrands
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllBrandsViewModel
    {
        public IEnumerable<BrandLookupModel> Brands { get; set; }
    }
}
