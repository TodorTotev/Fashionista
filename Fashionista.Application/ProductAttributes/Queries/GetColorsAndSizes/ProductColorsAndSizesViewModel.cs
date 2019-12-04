namespace Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class ProductColorsAndSizesViewModel
    {
        public List<ProductSizeLookupModel> Sizes { get; set; }

        public List<ProductColorLookupModel> Colors { get; set; }
    }
}
