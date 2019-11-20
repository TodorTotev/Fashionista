namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllMainCategoriesSelectListViewModel
    {
        public IEnumerable<MainCategoryLookupModel> MainCategories { get; set; }
    }
}