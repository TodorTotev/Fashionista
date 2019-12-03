//namespace Fashionista.Web.Controllers
//{
//    using System.Threading.Tasks;
//
//    using Fashionista.Application.Common.Models.Category;
//    using Fashionista.Application.Products.Queries.GetAllProductsByCategory;
//    using Microsoft.AspNetCore.Mvc;
//
//    public class CategoryController : BaseController
//    {
//        public async Task<IActionResult> Index(CategoryProductsViewModel model)
//        {
//            var products = this.Mediator.Send(new GetAllProductsByCategoryQuery { Id = model.Id });
//            
//        }
//    }
//}