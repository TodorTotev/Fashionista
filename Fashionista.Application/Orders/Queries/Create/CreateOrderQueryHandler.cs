namespace Fashionista.Application.Orders.Queries.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Orders.Commands.Create;
    using Fashionista.Domain.Entities;
    using MediatR;
    using X.PagedList;

    public class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, CreateOrderCommand>
    {
        private readonly IUserAssistant userAssistant;
        private readonly IDeletableEntityRepository<Address> addressesRepository;

        public CreateOrderQueryHandler(
            IUserAssistant userAssistant,
            IDeletableEntityRepository<Address> addressesRepository)
        {
            this.userAssistant = userAssistant;
            this.addressesRepository = addressesRepository;
        }

        public async Task<CreateOrderCommand> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var addresses = await this.addressesRepository
                .All()
                .Where(x => x.ApplicationUserId == this.userAssistant.UserId)
                .To<AddressLookupModel>()
                .ToListAsync(cancellationToken);

            return new CreateOrderCommand
            {
                CustomerInformation = this.userAssistant.FullName,
                Addresses = addresses,
            };
        }
    }
}
