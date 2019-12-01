namespace Fashionista.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using X.PagedList;

    public class Wishlist : PageModel
    {
        private readonly IMediator mediator;

        public Wishlist(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public StaticPagedList<ProductLookupModel> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public async Task<IActionResult> OnGetAsync()
        {
            this.Page -= 1;

            var query = new GetAllWishlistProductsPagedQuery
                { PageNumber = this.Page, PageSize = this.PageSize };

            var viewModel = await this.mediator.Send(query);
            var pagedList = new StaticPagedList<ProductLookupModel>(
                viewModel.Products, this.Page + 1, pageSize: this.PageSize, viewModel.Products.Count());

            this.Products = pagedList;

            return this.Page();
        }
    }
}
