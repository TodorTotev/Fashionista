namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class SubCategoriesController : AdministrationController
    {
        public async Task<IActionResult> Create()
        {
            return this.NotFound();
        }
    }
}