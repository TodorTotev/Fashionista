namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesNavigation;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class NavigationViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public NavigationViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await this.mediator.Send(new GetAllMainCategoriesNavigationQuery());

            return this.View(model);
        }
    }
}