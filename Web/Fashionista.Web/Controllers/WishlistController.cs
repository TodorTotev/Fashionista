namespace Fashionista.Web.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.Users.Queries.GetUser;
    using Fashionista.Application.Wishlist.Commands.Create;
    using Fashionista.Application.Wishlist.Commands.Delete;
    using Microsoft.AspNetCore.Mvc;

    public class WishlistController : BaseController
    {
        [Route("Identity/Account/Manage/CreateFavoriteProduct")]
        public async Task<IActionResult> Create(CreateFavoriteProductCommand command)
        {
            await this.Mediator.Send(command);

            return this.Redirect("Wishlist");
        }

        [Route("Identity/Account/Manage/DeleteFavoriteProduct")]
        public async Task<IActionResult> Delete(DeleteFavoriteProductCommand command)
        {
            command.User = await this.Mediator.Send(new GetUserQuery { Principal = this.User });
            await this.Mediator.Send(command);

            return this.Redirect("Wishlist");
        }
    }
}