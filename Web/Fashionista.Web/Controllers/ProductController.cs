namespace Fashionista.Web.Controllers
{
    using System.Threading.Tasks;

    using Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes;
    using Fashionista.Application.Products.Commands.AddReview;
    using Fashionista.Application.Products.Queries.Details;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : BaseController
    {
        public async Task<IActionResult> Details(GetProductDetailsQuery query)
        {
            var model = await this.Mediator.Send(query);
            model.Attributes = await this.Mediator.Send(new GetColorsAndSizesQuery { Id = query.Id });

            return this.View(model);
        }

        public async Task<IActionResult> Rate(AddReviewCommand command)
        {
            await this.Mediator.Send(command);

            return this.RedirectToAction("Details", new { id = command.Id });
        }
    }
}
