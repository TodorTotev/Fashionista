namespace Fashionista.Infrastructure
{
    using System;
    using System.Security.Claims;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class UserAssistant : IUserAssistant
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly UserManager<ApplicationUser> userManager;

        public UserAssistant(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.userManager = userManager;
        }

        public ClaimsPrincipal User => this.httpContextAccessor.HttpContext?.User;

        public int ShoppingCartId => this.userManager.GetUserAsync(this.User).Result.ShoppingCartId;

        public string UserId => this.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Annonymous";

        public string Username => this.User?.FindFirstValue(ClaimTypes.Name) ?? "John Doe";
    }
}
