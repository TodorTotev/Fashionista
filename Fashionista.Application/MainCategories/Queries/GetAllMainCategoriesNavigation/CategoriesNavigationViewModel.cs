namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesNavigation
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class CategoriesNavigationViewModel
    {
        public IEnumerable<MainCategoryLookupModel> Categories { get; set; }
    }
}