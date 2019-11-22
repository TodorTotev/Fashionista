namespace Fashionista.Application.ProductColors.Queries.GetAllColorsSelectList
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GetAllColorsSelectListViewModel
    {
        public IEnumerable<SelectListItem> AllColors { get; set; }
    }
}
