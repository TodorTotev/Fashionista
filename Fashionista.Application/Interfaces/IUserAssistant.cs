namespace Fashionista.Application.Interfaces
{
    using System.Security.Claims;

    using Fashionista.Domain.Entities;

    public interface IUserAssistant
    {
        ClaimsPrincipal User { get; }

        int ShoppingCartId { get;  }

        string UserId { get; }

        string Username { get; }
    }
}
