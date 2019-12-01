namespace Fashionista.Application.Users.Queries.GetUser
{
    using System.Security.Claims;

    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetUserQuery : IRequest<ApplicationUser>
    {
        public ClaimsPrincipal Principal { get; set; }
    }
}
