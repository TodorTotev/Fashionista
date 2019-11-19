using Microsoft.AspNetCore.Mvc;

namespace Fashionista.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult TermsOfService()
        {
            return this.View();
        }

        public IActionResult Shipping()
        {
            return this.View();
        }

        public IActionResult Returns()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
