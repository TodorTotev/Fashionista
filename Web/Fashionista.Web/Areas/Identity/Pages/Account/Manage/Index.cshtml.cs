namespace Fashionista.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Threading.Tasks;

    using Fashionista.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var email = await this.userManager.GetEmailAsync(user);

            if (this.Input == null)
            {
                this.Input = new InputModel();
            }

            this.Input.FirstName = user.FirstName;
            this.Input.LastName = user.LastName;
            this.Input.Email = email;

            return this.Page();
        }

        public class InputModel
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
        }
    }
}
