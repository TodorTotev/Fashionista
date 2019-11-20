namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.MainCategories.Commands.Create;
    using Fashionista.Application.MainCategories.Commands.Delete;
    using Fashionista.Application.MainCategories.Commands.Edit;
    using Fashionista.Application.MainCategories.Queries.Edit;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategories;
    using Microsoft.AspNetCore.Mvc;

    public class MainCategoriesController : AdministrationController
    {
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.Mediator.Send(new GetAllMainCategoriesQuery());

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMainCategoryCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(EditMainCategoryQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMainCategoryCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var action = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(DeleteMainCategoryCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
