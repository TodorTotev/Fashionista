namespace Fashionista.Application.Addresses.Queries.GetAllUserAddresses
{
    using Fashionista.Application.Common.Models;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetAllUserAddressesQuery : IRequest<GetAllUserAddressesViewModel>
    {
        public ApplicationUser User { get; set; }
    }
}
