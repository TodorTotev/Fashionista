namespace Fashionista.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Threading.Tasks;

    using Fashionista.Application.Addresses.Queries.GetAllUserAddresses;
    using Fashionista.Application.Users.Queries.GetUser;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class Addresses : PageModel
    {
        private readonly IMediator mediator;

        public Addresses(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public GetAllUserAddressesViewModel AddressList { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.mediator.Send(new GetUserQuery { Principal = this.User });
            var addresses = await this.mediator.Send(new GetAllUserAddressesQuery { User = user });

            this.AddressList = addresses;

            return this.Page();
        }

        public class InputModel
        {
            public GetAllUserAddressesViewModel AddressList { get; set; }
        }
    }
}
