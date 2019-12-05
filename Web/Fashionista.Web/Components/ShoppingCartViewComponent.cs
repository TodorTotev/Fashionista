namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ShoppingCartViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await this.mediator.Send(new GetAllShoppingCartProductsQuery());

            return this.View(model);
        }
    }
}
