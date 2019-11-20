namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Brands.Commands.Create;
    using Fashionista.Application.Brands.Commands.Delete;
    using Fashionista.Application.Brands.Queries.GetAllBrandsPaged;
    using Fashionista.Application.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    public class BrandsController : AdministrationController
    {
        public async Task<IActionResult> Index(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 10;
            var query = new GetAllBrandsPagedQuery { PageNumber = page.Value, PageSize = pageSize };

            var viewModel = await this.Mediator.Send(query);
            var pagedList = new StaticPagedList<BrandLookupModel>(
                viewModel.Brands, page.Value + 1, pageSize, viewModel.Brands.Count());

            return this.View(pagedList);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(DeleteBrandCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
