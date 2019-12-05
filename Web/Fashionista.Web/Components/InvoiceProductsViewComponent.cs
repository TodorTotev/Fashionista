namespace Fashionista.Web.Components
{
    using System.Threading.Tasks;

    using Fashionista.Application.Orders.Queries.GetOrderProducts;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class InvoiceProductsViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public InvoiceProductsViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int orderId)
        {
            var model = await this.mediator.Send(new GetOrderProductsByOrderIdQuery { Id = orderId });
            return this.View(model);
        }
    }
}
