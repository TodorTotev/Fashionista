namespace Fashionista.Application.Interfaces
{
    using System.Security.Claims;

    public interface IUserAssistant
    {
        ClaimsPrincipal User { get; }

        int ShoppingCartId { get;  }

        string UserId { get; }

        string Username { get; }
    }
}
