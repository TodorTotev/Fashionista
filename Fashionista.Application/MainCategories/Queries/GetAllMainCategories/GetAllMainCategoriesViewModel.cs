namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategories
{
    using System;
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllMainCategoriesViewModel
    {
        public IEnumerable<MainCategoryLookupModel> MainCategories { get; set; }
    }
}
