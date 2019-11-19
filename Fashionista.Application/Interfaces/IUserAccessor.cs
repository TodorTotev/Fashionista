namespace Fashionista.Application.Interfaces
{
    using System.Security.Claims;

    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }

        string UserId { get; }

        string Username { get; }
    }
}
