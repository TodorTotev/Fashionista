namespace Fashionista.Web.Middlewares
{
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (BaseCustomException ex)
            {
                if (ex is NotFoundException)
                {
                    var result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/NotFound.cshtml",
                    };

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.WriteResultAsync(result);
                    }
                }
                else if (ex is ForbiddenException)
                {
                    var result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Forbidden.cshtml",
                    };

                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.WriteResultAsync(result);
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status204NoContent;
                }
            }
        }
    }
}
