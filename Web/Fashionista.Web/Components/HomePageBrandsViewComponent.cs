namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.Brands.Queries.GetAllBrands;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class HomePageBrandsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public HomePageBrandsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await this.mediator.Send(new GetAllBrandsQuery());

            return this.View(model);
        }
    }
}
