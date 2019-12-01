namespace Fashionista.Application.Addresses.Queries.GetAllUserAddresses
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetAllUserAddressesQueryHandler : IRequestHandler<GetAllUserAddressesQuery, GetAllUserAddressesViewModel>
    {
        private readonly IDeletableEntityRepository<Address> addressesRepository;
        private readonly IMapper mapper;
        private readonly IUserAssistant userAssistant;

        public GetAllUserAddressesQueryHandler(
            IDeletableEntityRepository<Address> addressesRepository,
            IMapper mapper,
            IUserAssistant userAssistant)
        {
            this.addressesRepository = addressesRepository;
            this.mapper = mapper;
            this.userAssistant = userAssistant;
        }

        public async Task<GetAllUserAddressesViewModel> Handle(GetAllUserAddressesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var addresses = await this.addressesRepository
                .All()
                .Where(x => x.ApplicationUserId == this.userAssistant.UserId)
                .ProjectTo<AddressLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllUserAddressesViewModel
            {
                Addresses = addresses,
            };
        }
    }
}
