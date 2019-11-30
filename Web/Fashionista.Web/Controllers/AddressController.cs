namespace Fashionista.Web.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.Addresses.Commands.Create;
    using Fashionista.Application.Addresses.Commands.Delete;
    using Fashionista.Application.Users.Queries.GetUser;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AddressController : BaseController
    {
        [Route("Identity/Address/Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Route("Identity/Address/Create")]
        public async Task<IActionResult> Create(CreateAddressCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.User = await this.Mediator.Send(new GetUserQuery { Principal = this.User });
                return this.View(command);
            }

            command.User = await this.Mediator.Send(new GetUserQuery { Principal = this.User });

            await this.Mediator.Send(command);

            return this.RedirectToPage("Addresses");
        }

        public async Task<IActionResult> Delete(DeleteAddressCommand command)
        {
            await this.Mediator.Send(command);

            return this.RedirectToPage("Addresses");
        }
    }
}
