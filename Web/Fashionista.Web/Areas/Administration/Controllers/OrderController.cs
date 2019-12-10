namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.Orders.Queries.GetProcessed;
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : AdministrationController
    {
        public async Task<IActionResult> Processed()
        {
            var model = await this.Mediator.Send(new GetProcessedOrdersQuery { IsDelivered = false });

            return this.View(model);
        }

        public async Task<IActionResult> Delivered()
        {
            var model = await this.Mediator.Send(new GetProcessedOrdersQuery());

            return this.View(model);
        }
    }
}
