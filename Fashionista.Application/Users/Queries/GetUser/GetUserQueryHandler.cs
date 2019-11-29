namespace Fashionista.Application.Users.Queries.GetUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GetUserQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<ApplicationUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            return this.userManager.GetUserAsync(request.Principal);
        }
    }
}
