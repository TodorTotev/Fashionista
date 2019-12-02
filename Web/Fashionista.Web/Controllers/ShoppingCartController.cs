using Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts;

namespace Fashionista.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.ShoppingCart.Commands.Add;
    using Fashionista.Application.ShoppingCart.Commands.AddSesssion;
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using Fashionista.Common;
    using Fashionista.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var model = await this.Mediator.Send(new GetAllShoppingCartProductsQuery());

                if (!model.ShoppingCartProducts.Any())
                {
                    return this.Redirect("/");
                }

                return this.View(model);
            }

            var session = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            if (session == null || !session.Any())
            {
                return this.Redirect("/");
            }

            return this.View(new AllShoppingCartProductsViewModel
            {
                ShoppingCartProducts = session,
            });
        }

        public async Task<IActionResult> Add(AddProductInCartCommand command)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this.Mediator.Send(command);
                return this.RedirectToAction(nameof(this.Index));
            }

            var products = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            var session = await this.Mediator.Send(new AddSessionProductCartCommand { Id = command.Id, Session = products });

            if (session == null)
            {
                return this.Redirect("/");
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
