namespace Fashionista.Web.Controllers
{
    using System.Threading.Tasks;
    using Fashionista.Application.Addresses.Commands.Create;
    using Fashionista.Application.Addresses.Commands.Delete;
    using Fashionista.Application.Addresses.Queries.GetAllUserAddresses;
    using Fashionista.Application.Users.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AddressController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var user = await this.Mediator.Send(new GetUserQuery {Principal = this.User});
            var viewModel = await this.Mediator.Send(new GetAllUserAddressesQuery {User = user});

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                command.User = await this.Mediator.Send(new GetUserQuery { Principal = this.User });
                return this.View(command);
            }

            command.User = await this.Mediator.Send(new GetUserQuery { Principal = this.User });

            await this.Mediator.Send(command);

            return this.NotFound();
        }

        public async Task<IActionResult> Delete(DeleteAddressCommand command)
        {
            await this.Mediator.Send(command);

            return this.NotFound();
        }
    }
}