namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts;
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartBasketViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public ShoppingCartBasketViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var model = await this.mediator.Send(new GetAllShoppingCartProductsQuery());
                return this.View(model);
            }

            var sessionModel = await this.mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            return this.View(sessionModel);
        }
    }
}
