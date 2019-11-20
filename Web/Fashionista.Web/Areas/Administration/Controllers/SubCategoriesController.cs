namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList;
    using Fashionista.Application.SubCategories.Commands.Create;
    using Fashionista.Application.SubCategories.Commands.Delete;
    using Fashionista.Application.SubCategories.Commands.Edit;
    using Fashionista.Application.SubCategories.Queries.Edit;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategories;
    using Microsoft.AspNetCore.Mvc;

    public class SubCategoriesController : AdministrationController
    {
        public async Task<IActionResult> Index()
        {
            var viewModel = await this.Mediator.Send(new GetAllSubCategoriesQuery());
            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var query = await this.Mediator.Send(new GetAllMainCategoriesSelectListQuery());
            var viewModel = new CreateSubCategoryCommand { AllMainCategoriesSelectList = query };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubCategoryCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                var query = await this.Mediator.Send(new GetAllMainCategoriesSelectListQuery());
                command.AllMainCategoriesSelectList = query;
                return this.View(command);
            }

            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(EditSubCategoryQuery query)
        {
            var viewModel = await this.Mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSubCategoryCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var action = await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(DeleteSubCategoryCommand command)
        {
            await this.Mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
