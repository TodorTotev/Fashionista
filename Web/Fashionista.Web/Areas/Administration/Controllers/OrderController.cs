using Fashionista.Application.Orders.Commands.SendOrder;

namespace Fashionista.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Orders.Queries.GetProcessed;
    using Microsoft.AspNetCore.Mvc;
    using X.PagedList;

    public class OrderController : AdministrationController
    {
        public async Task<IActionResult> Processed(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 20;

            var query = new GetProcessedOrdersPagedQuery { IsDelivered = false, PageNumber = page.Value, PageSize = pageSize };

            var viewModel = await this.Mediator.Send(query);
            var pagedList = new StaticPagedList<OrderLookupModel>(
                viewModel.CurrentOrders, page.Value + 1, pageSize, viewModel.CurrentOrders.Count());

            return this.View(pagedList);
        }

        public async Task<IActionResult> Delivered(int? page)
        {
            page = (page ?? 1) - 1;
            const int pageSize = 20;

            var query = new GetProcessedOrdersPagedQuery { IsDelivered = true, PageNumber = page.Value, PageSize = pageSize };

            var viewModel = await this.Mediator.Send(query);
            var pagedList = new StaticPagedList<OrderLookupModel>(
                viewModel.CurrentOrders, page.Value + 1, pageSize, viewModel.CurrentOrders.Count());

            return this.View(pagedList);
        }

        public async Task<IActionResult> Send(SendOrderCommand command)
        {
            await this.Mediator.Send(command);

            return this.RedirectToAction(nameof(this.Processed));
        }
    }
}
