namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.Brands.Queries.GetAllBrandsSelectList;
    using Fashionista.Application.Products.Commands.Create;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : AdministrationController
    {
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
            return this.View();
        }
    }
}