namespace Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GetAllSubCategoriesSelectListViewModel
    {
        public IEnumerable<SelectListItem> SubCategories { get; set; }
    }
}
