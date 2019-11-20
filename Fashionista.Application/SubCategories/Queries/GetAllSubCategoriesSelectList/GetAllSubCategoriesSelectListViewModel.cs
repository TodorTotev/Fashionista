namespace Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllSubCategoriesSelectListViewModel
    {
        public IEnumerable<SubCategoryLookupModel> SubCategories { get; set; }
    }
}
