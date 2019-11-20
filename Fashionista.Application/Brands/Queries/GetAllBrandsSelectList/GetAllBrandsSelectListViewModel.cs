namespace Fashionista.Application.Brands.Queries.GetAllBrandsSelectList
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllBrandsSelectListViewModel
    {
        public IEnumerable<BrandLookupModel> Brands { get; set; }
    }
}