namespace Fashionista.Application.Cities.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, int>
    {
        private readonly IDeletableEntityRepository<City> citiesRepository;
        private readonly IMapper mapper;

        public CreateCityCommandHandler(
            IDeletableEntityRepository<City> citiesRepository,
            IMapper mapper)
        {
            this.citiesRepository = citiesRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            if (await CommonCheckAssistant.CheckIfCityWithSameNameExists(request.Name, this.citiesRepository))
            {
                throw new EntityAlreadyExistsException(nameof(City), request.Name);
            }

            var city = this.mapper.Map<City>(request);
            await this.citiesRepository.AddAsync(city);
            await this.citiesRepository.SaveChangesAsync(cancellationToken);

            return city.Id;
        }
    }
}
