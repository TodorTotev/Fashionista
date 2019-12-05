namespace Fashionista.Application.Orders.Queries.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Orders.Commands.Create;
    using Fashionista.Domain.Entities;
    using MediatR;
    using X.PagedList;

    public class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, CreateOrderCommand>
    {
        private readonly IUserAssistant userAssistant;
        private readonly IDeletableEntityRepository<Address> addressesRepository;
        private readonly IMapper mapper;

        public CreateOrderQueryHandler(
            IUserAssistant userAssistant,
            IDeletableEntityRepository<Address> addressesRepository,
            IMapper mapper)
        {
            this.userAssistant = userAssistant;
            this.addressesRepository = addressesRepository;
            this.mapper = mapper;
        }

        public async Task<CreateOrderCommand> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var addresses = await this.addressesRepository
                .All()
                .Where(x => x.ApplicationUserId == this.userAssistant.UserId)
                .ProjectTo<AddressLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CreateOrderCommand
            {
                CustomerInformation = this.userAssistant.FullName,
                Addresses = addresses,
            };
        }
    }
}
