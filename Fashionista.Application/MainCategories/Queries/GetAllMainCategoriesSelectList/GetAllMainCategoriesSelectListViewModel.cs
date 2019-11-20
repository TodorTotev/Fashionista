namespace Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GetAllMainCategoriesSelectListViewModel
    {
        public IEnumerable<SelectListItem> MainCategories { get; set; }
    }
}