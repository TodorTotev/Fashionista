namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.ProductColors.Commands.Create;
    using Microsoft.AspNetCore.Mvc;

    public class ProductColorController : AdministrationController
    {
        public IActionResult Create(int id)
        {
            var command = new CreateProductColorCommand { ProductId = id };
            return this.View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductColorCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction("AddAttributes", "Products", new { Id = command.ProductId });
        }
    }
}
