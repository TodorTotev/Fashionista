using Fashionista.Application.MainCategories.Commands.Delete;
using Fashionista.Application.MainCategories.Commands.Edit;
using Fashionista.Application.MainCategories.Queries.Edit;

namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.MainCategories.Commands.Create;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategories;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class MainCategoriesController : AdministrationController
    {
        private readonly IMediator mediator;

        public MainCategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.mediator.Send(new GetAllMainCategoriesQuery());

            return this.View(model);
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

            await this.mediator.Send(command);

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(EditMainCategoryQuery query)
        {
            var viewModel = await this.mediator.Send(query);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMainCategoryCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var action = await this.mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(DeleteMainCategoryCommand command)
        {
            await this.mediator.Send(command);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
