namespace Fashionista.Application.Brands.Queries.GetAllBrandsSelectList
{
    using System.Collections.Generic;

    using Fashionista.Application.Common.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GetAllBrandsSelectListViewModel
    {
        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}