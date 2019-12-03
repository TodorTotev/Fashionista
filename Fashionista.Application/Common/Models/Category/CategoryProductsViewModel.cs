namespace Fashionista.Application.Common.Models.Category
{
    using System.Collections.Generic;

    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;

    public class CategoryProductsViewModel
    {
        public int Id { get; set; }

        public int? BrandId { get; set; }

        public int? Page { get; set; }

        public ProductSort SortCondition { get; set; }

        public ProductSize SortSize { get; set; }

        public ProductType SortGender { get; set; }

        public ProductColor SortColor { get; set; }

        public IEnumerable<ProductLookupModel> Products { get; set; }

        public IEnumerable<BrandLookupModel> Brands { get; set; }
    }
}