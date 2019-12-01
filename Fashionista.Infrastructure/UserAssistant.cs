namespace Fashionista.Infrastructure
{
    using System;
    using System.Security.Claims;

    using Fashionista.Application.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class UserAssistant : IUserAssistant
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAssistant(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public ClaimsPrincipal User => this.httpContextAccessor.HttpContext?.User;

        public string UserId => this.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Annonymous";

        public string Username => this.User?.FindFirstValue(ClaimTypes.Name) ?? "John Doe";
    }
}
