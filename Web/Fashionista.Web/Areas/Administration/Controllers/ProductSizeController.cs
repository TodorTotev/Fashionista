namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList;
    using Fashionista.Application.ProductSizes.Commands.Create;
    using Microsoft.AspNetCore.Mvc;

    public class ProductSizeController : AdministrationController
    {
        public async Task<IActionResult> Create(int id)
        {
            var categories = await this.Mediator.Send(new GetAllMainCategoriesSelectListQuery());
            var command = new CreateProductSizeCommand { MainCategoriesSelectList = categories, ProductId = id };

            return this.View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductSizeCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.MainCategoriesSelectList = await this.Mediator.Send(new GetAllMainCategoriesSelectListQuery());
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction("AddAttributes", "Products", new { Id = command.ProductId });
        }
    }
}
