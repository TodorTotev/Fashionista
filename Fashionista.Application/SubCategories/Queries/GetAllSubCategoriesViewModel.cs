namespace Fashionista.Application.SubCategories.Queries
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;

    public class GetAllSubCategoriesViewModel
    {
        public IEnumerable<SubCategoryLookupModel> SubCategories { get; set; }
    }
}
