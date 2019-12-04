namespace Fashionista.Application.Common.Models.Category
{
    using System.Collections.Generic;

    using Fashionista.Application.Brands.Queries.GetAllBrands;
    using Fashionista.Domain.Entities.Enums;
    using X.PagedList;

    public class CategoryProductsViewModel
    {
        public int Id { get; set; }

        public int? BrandId { get; set; }

        public int? Page { get; set; }

        public ProductSort ProductSort { get; set; }

        public int SizeId { get; set; }

        public ProductType SortGender { get; set; }

        public int ColorId { get; set; }

        public SubCategoryLookupModel SubCategory { get; set; }

        public IEnumerable<ProductSizeLookupModel> Sizes { get; set; }

        public IEnumerable<ProductColorLookupModel> Colors { get; set; }

        public StaticPagedList<ProductLookupModel> Products { get; set; }

        public GetAllBrandsViewModel ListOfBrands { get; set; }
    }
}
