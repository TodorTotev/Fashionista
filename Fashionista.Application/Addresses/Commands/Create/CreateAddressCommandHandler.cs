namespace Fashionista.Application.Addresses.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Cities.Commands;
    using Fashionista.Application.Cities.Queries.GetCity;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, int>
    {
        private readonly IDeletableEntityRepository<Address> addressesRepository;
        private readonly IMediator mediator;

        public CreateAddressCommandHandler(
            IDeletableEntityRepository<Address> addressesRepository,
            IMediator mediator)
        {
            this.addressesRepository = addressesRepository;
            this.mediator = mediator;
        }

        public async Task<int> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            var city = this.mediator.Send(new GetCityQuery { Name = request.City })
                       ?? this.mediator.Send(new CreateCityCommand { Name = request.City, Postcode = request.Zip });

            var address = new Address
            {
                CityId = city.Id,
                Name = request.Street,
                Description = request.Description,
                ApplicationUserId = request.User.Id,
            };

            await this.addressesRepository.AddAsync(address);
            await this.addressesRepository.SaveChangesAsync(cancellationToken);

            return address.Id;
        }
    }
}
