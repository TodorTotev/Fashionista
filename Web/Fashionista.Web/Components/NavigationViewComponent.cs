using MediatR;

namespace Fashionista.Web.Components
{
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
            var model = new N
        }
    }
}