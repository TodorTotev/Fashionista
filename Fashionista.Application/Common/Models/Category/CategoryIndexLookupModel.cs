namespace Fashionista.Application.Common.Models.Category
{
    using System.Collections.Generic;

    public class CategoryIndexLookupModel
    {
        public IEnumerable<ProductLookupModel> Products { get; set; }

        public IEnumerable<BrandLookupModel> Brands { get; set; }
    }
}