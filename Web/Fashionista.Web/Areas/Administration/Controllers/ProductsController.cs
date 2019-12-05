using Fashionista.Application.ProductAttributes.Commands.Delete;
using Fashionista.Application.ProductAttributes.Commands.Edit;
using Fashionista.Application.ProductAttributes.Queries.Edit;

namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Brands.Queries.GetAllBrandsSelectList;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using Fashionista.Application.ProductAttributes.Queries.Create;
    using Fashionista.Application.ProductAttributes.Queries.GetAll;
    using Fashionista.Application.ProductColors.Queries.GetAllColorsSelectList;
    using Fashionista.Application.Products.Commands.Create;
    using Fashionista.Application.Products.Commands.Delete;
    using Fashionista.Application.Products.Commands.Edit;
    using Fashionista.Application.Products.Queries.Edit;
    using Fashionista.Application.Products.Queries.GetAllProductsPaged;
    using Fashionista.Application.ProductSizes.Queries.GetAllSizesSelectList;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    public class ProductsController : AdministrationController
    {
        public async Task<IActionResult> Index(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 20;
            var query = new GetAllProductsPagedQuery { PageNumber = page.Value, PageSize = pageSize, IsActive = true };

            var viewModel = await this.Mediator.Send(query);
            var pagedList = new StaticPagedList<ProductLookupModel>(
                viewModel.Products, page.Value + 1, pageSize, viewModel.Products.Count());

            return this.View(pagedList);
        }

        public async Task<IActionResult> Draft(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 20;
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

            var id = await this.Mediator.Send(command);
            return this.RedirectToAction("AddAttributes", new { Id = id });
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

        public async Task<IActionResult> Attributes(int id)
        {
            var viewModel = await this.Mediator.Send(new GetAllProductAttributesQuery { Id = id });
            return this.View(viewModel);
        }

        public async Task<IActionResult> AddAttributes(CreateProductAttributeQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            viewModel.ColorsSelectListViewModel = await this.Mediator.Send(new GetAllColorsSelectListQuery());
            viewModel.SizesSelectListViewModel = await this.Mediator
                .Send(new GetAllSizesSelectListQuery { MainCategoryId = viewModel.MainCategoryId });

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttributes(CreateProductAttributeCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.ColorsSelectListViewModel = await this.Mediator.Send(new GetAllColorsSelectListQuery());
                command.SizesSelectListViewModel = await this.Mediator
                    .Send(new GetAllSizesSelectListQuery { MainCategoryId = command.MainCategoryId });
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction("Attributes", "Products", new { Id = command.ProductId });
        }

        public async Task<IActionResult> EditAttributes(EditProductAttributeQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAttributes(EditProductAttributeCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction("Attributes", "Products", new { Id = command.ProductId });
        }

        public async Task<IActionResult> DeleteAttribute(DeleteProductAttributeCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction("Attributes", "Products", new { Id = command.ProductId });
        }
    }
}
