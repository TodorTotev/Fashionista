namespace Fashionista.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Threading.Tasks;

    using Fashionista.Application.Addresses.Queries.GetAllUserAddresses;
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
            var addresses = await this.mediator.Send(new GetAllUserAddressesQuery());

            this.AddressList = addresses;

            return this.Page();
        }

        public class InputModel
        {
            public GetAllUserAddressesViewModel AddressList { get; set; }
        }
    }
}
