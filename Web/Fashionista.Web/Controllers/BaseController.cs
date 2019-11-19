namespace Fashionista.Web.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator
            => this.mediator ?? this.HttpContext.RequestServices.GetService<IMediator>();
    }
}
