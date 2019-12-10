namespace Fashionista.Web.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.Addresses.Commands.Create;
    using Fashionista.Application.Addresses.Commands.Delete;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AddressController : BaseController
    {
        [Route("Identity/Account/Manage/CreateAddress")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Route("Identity/Account/Manage/CreateAddress")]
        public async Task<IActionResult> Create(CreateAddressCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(command);
            }

            await this.Mediator.Send(command);
            await Task.Delay(1500);
            return this.Redirect("Addresses");
        }

        [Route("Identity/Account/Manage/Delete")]
        public async Task<IActionResult> Delete(DeleteAddressCommand command)
        {
            await this.Mediator.Send(command);

            await Task.Delay(1500);
            return this.Redirect("Addresses");
        }
    }
}
