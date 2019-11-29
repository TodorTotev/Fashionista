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

        public GetAllUserAddressesQueryHandler(IDeletableEntityRepository<Address> addressesRepository, IMapper mapper)
        {
            this.addressesRepository = addressesRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllUserAddressesViewModel> Handle(GetAllUserAddressesQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var addresses = await this.addressesRepository
                .All()
                .Where(x => x.ApplicationUserId == request.User.Id)
                .ProjectTo<AddressLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetAllUserAddressesViewModel
            {
                Addresses = addresses,
            };
        }
    }
}
