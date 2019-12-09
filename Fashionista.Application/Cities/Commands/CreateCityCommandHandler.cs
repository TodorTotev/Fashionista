namespace Fashionista.Application.Cities.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, City>
    {
        private readonly IDeletableEntityRepository<City> citiesRepository;

        public CreateCityCommandHandler(
            IDeletableEntityRepository<City> citiesRepository)
        {
            this.citiesRepository = citiesRepository;
        }

        public async Task<City> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            if (await CommonCheckAssistant.CheckIfCityWithSameNameExists(request.Name, this.citiesRepository))
            {
                throw new EntityAlreadyExistsException(nameof(City), request.Name);
            }

            var city = request.To<City>();
            await this.citiesRepository.AddAsync(city);
            await this.citiesRepository.SaveChangesAsync(cancellationToken);

            return city;
        }
    }
}
