using Fashionista.Application.ShoppingCart.Commands.Clear;
using Fashionista.Application.ShoppingCart.Commands.ClearSession;

namespace Fashionista.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.ShoppingCart.Commands.Add;
    using Fashionista.Application.ShoppingCart.Commands.AddSesssion;
    using Fashionista.Application.ShoppingCart.Commands.Delete;
    using Fashionista.Application.ShoppingCart.Commands.DeleteSession;
    using Fashionista.Application.ShoppingCart.Commands.Edit;
    using Fashionista.Application.ShoppingCart.Commands.EditSession;
    using Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts;
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
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

            var products = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            if (products == null || !products.Any())
            {
                return this.Redirect("/");
            }

            return this.View(new AllShoppingCartProductsViewModel
            {
                ShoppingCartProducts = products,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductInCartCommand command)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this.Mediator.Send(command);
                return this.RedirectToAction(nameof(this.Index));
            }

            var products = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            if (products == null)
            {
                return this.Redirect("/");
            }

            await this.Mediator.Send(new AddSessionProductInCartCommand
                {
                    Id = command.Id,
                    Session = products,
                    SizeId = command.SizeId,
                    ColorId = command.ColorId,
                    Quantity = command.Quantity,
                });

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(EditShoppingCartProductCommand command)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this.Mediator.Send(command);
                return this.RedirectToAction(nameof(this.Index));
            }

            var products = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            if (products == null || !products.Any())
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.Mediator.Send(new EditSessionShoppingCartProductCommand
                { Id = command.Id, Quantity = command.Quantity, Session = products });

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(DeleteProductFromCartCommand command)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this.Mediator.Send(command);
                return this.RedirectToAction(nameof(this.Index));
            }

            var products = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());

            if (products == null || !products.Any())
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.Mediator.Send(new DeleteSessionProductFromCartCommand { Id = command.Id, Session = products });

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Clear(ClearShoppingCartCommand command)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                await this.Mediator.Send(command);
                return this.RedirectToAction(nameof(this.Index));
            }

            var products = await this.Mediator.Send(new GetAllSessionShoppingCartProductsQuery());
            await this.Mediator.Send(new ClearSessionShoppingCartCommand { Session = products });

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
