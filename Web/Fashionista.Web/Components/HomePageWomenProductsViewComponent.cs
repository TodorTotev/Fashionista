namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.Products.Queries.GetAllWomenProducts;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class HomePageWomenProductsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public HomePageWomenProductsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await this.mediator.Send(new GetAllWomenProductsQuery());

            return this.View(model);
        }
    }
}
