namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.Products.Queries.GetAllMenProducts;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class HomePageMenProductsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public HomePageMenProductsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await this.mediator.Send(new GetAllMenProductsQuery());

            return this.View(model);
        }
    }
}
