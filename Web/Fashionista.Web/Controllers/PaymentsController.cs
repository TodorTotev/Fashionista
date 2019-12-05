namespace Fashionista.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Stripe;

    public class PaymentsController : BaseController
    {
        [HttpPost]
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken,
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Description = "Test",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
            });

            if (charge.Status == "succeeded")
            {
                this.TempData["Payment"] = "Payment is completed!";
            }

            if (charge.Status == "failed")
            {
                this.TempData["Payment"] = "Your payment was refused!";
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
