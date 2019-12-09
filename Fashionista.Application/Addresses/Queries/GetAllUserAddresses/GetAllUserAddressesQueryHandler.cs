namespace Fashionista.Application.Addresses.Queries.GetAllUserAddresses
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllUserAddressesQueryHandler : IRequestHandler<GetAllUserAddressesQuery, GetAllUserAddressesViewModel>
    {
        private readonly IDeletableEntityRepository<Address> addressesRepository;
        private readonly IUserAssistant userAssistant;

        public GetAllUserAddressesQueryHandler(
            IDeletableEntityRepository<Address> addressesRepository,
            IUserAssistant userAssistant)
        {
            this.addressesRepository = addressesRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<GetAllUserAddressesViewModel> Handle(GetAllUserAddressesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var addresses = await this.addressesRepository
                .All()
                .Where(x => x.ApplicationUserId == this.userAssistant.UserId)
                .To<AddressLookupModel>()
                .ToListAsync(cancellationToken);

            return new GetAllUserAddressesViewModel
            {
                Addresses = addresses,
            };
        }
    }
}
