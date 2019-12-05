namespace Fashionista.Web.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.Orders.Commands.Cancel;
    using Fashionista.Application.Orders.Commands.Complete;
    using Fashionista.Application.Orders.Commands.Create;
    using Fashionista.Application.Orders.Queries.Complete;
    using Fashionista.Application.Orders.Queries.Create;
    using Fashionista.Application.Orders.Queries.Details;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OrdersController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            var command = await this.Mediator.Send(new CreateOrderQuery());

            return this.View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);

            return this.RedirectToAction(nameof(this.Review));
        }

        public async Task<IActionResult> Review()
        {
            var command = await this.Mediator.Send(new CompleteOrderQuery());

            return this.View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Review(CompleteOrderCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            var id = await this.Mediator.Send(command);

            return this.RedirectToAction("Details", new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await this.Mediator.Send(new GetOrderDetailsQuery { Id = id });

            return this.View(model);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            await this.Mediator.Send(new CancelOrderCommand { Id = id });

            return this.RedirectToAction("Index", "Home");
        }
    }
}
