namespace Fashionista.Application.Brands.Queries.Queries
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllBrandsPagedViewModel
    {
        public IEnumerable<BrandLookupModel> Brands { get; set; }
    }
}
