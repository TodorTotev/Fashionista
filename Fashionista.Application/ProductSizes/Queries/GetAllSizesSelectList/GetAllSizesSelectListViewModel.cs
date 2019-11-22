namespace Fashionista.Application.ProductSizes.Queries.GetAllSizesSelectList
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GetAllSizesSelectListViewModel
    {
        public IEnumerable<SelectListItem> AllSizes { get; set; }
    }
}
