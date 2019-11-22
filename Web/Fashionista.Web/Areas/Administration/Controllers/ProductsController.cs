using Fashionista.Application.ProductAttributes.Commands.Create;

namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Brands.Queries.GetAllBrandsSelectList;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Products.Commands.Create;
    using Fashionista.Application.Products.Commands.Delete;
    using Fashionista.Application.Products.Commands.Edit;
    using Fashionista.Application.Products.Queries.Edit;
    using Fashionista.Application.Products.Queries.GetAllProductsPaged;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    public class ProductsController : AdministrationController
    {
        public async Task<IActionResult> Index(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 10;
            var query = new GetAllProductsPagedQuery { PageNumber = page.Value, PageSize = pageSize, IsActive = true };

            var viewModel = await this.Mediator.Send(query);
            var pagedList = new StaticPagedList<ProductLookupModel>(
                viewModel.Products, page.Value + 1, pageSize, viewModel.Products.Count());

            return this.View(pagedList);
        }

        public async Task<IActionResult> Draft(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 10;
            var query = new GetAllProductsPagedQuery { PageNumber = page.Value, PageSize = pageSize, IsActive = false };

            var viewModel = await this.Mediator.Send(query);
            var pagedList = new StaticPagedList<ProductLookupModel>(
                viewModel.Products, page.Value + 1, pageSize, viewModel.Products.Count());

            return this.View(pagedList);
        }

        public async Task<IActionResult> Create()
        {
            return this.View(new CreateProductCommand
            {
                Brands = await this.Mediator.Send(new GetAllBrandsSelectListQuery()),
                SubCategories = await this.Mediator.Send(new GetAllSubCategoriesSelectListQuery()),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.Brands = await this.Mediator.Send(new GetAllBrandsSelectListQuery());
                command.SubCategories = await this.Mediator.Send(new GetAllSubCategoriesSelectListQuery());

                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(EditProductQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            viewModel.Brands = await this.Mediator.Send(new GetAllBrandsSelectListQuery());
            viewModel.SubCategories = await this.Mediator.Send(new GetAllSubCategoriesSelectListQuery());
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.Brands = await this.Mediator.Send(new GetAllBrandsSelectListQuery());
                command.SubCategories = await this.Mediator.Send(new GetAllSubCategoriesSelectListQuery());
                return this.View(command);
            }

            var action = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

//        public async Task<IActionResult> AddAttributes(CreateProductAttributeCommand request)
//        {
//            
//        }
    }
}
