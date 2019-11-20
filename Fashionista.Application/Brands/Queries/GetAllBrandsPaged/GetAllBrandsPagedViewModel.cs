namespace Fashionista.Application.Brands.Queries.GetAllBrandsPaged
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllBrandsPagedViewModel
    {
        public IEnumerable<BrandLookupModel> Brands { get; set; }
    }
}
