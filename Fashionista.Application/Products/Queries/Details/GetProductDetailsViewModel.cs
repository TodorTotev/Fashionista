namespace Fashionista.Application.Products.Queries.Details
{
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes;

    public class GetProductDetailsViewModel
    {
       public ProductLookupModel Product { get; set; }

       public ProductColorsAndSizesViewModel Attributes { get; set; }

       public int ColorId { get; set; }

       public int SizeId { get; set; }

       public int Quantity { get; set; }
    }
}
