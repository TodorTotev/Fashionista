namespace Fashionista.Application.Cities.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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

            if (await this.CheckIfCityWithSameNameExists(request.Name))
            {
                throw new EntityAlreadyExistsException(nameof(City), request.Name);
            }

            var city = request.To<City>();
            await this.citiesRepository.AddAsync(city);
            await this.citiesRepository.SaveChangesAsync(cancellationToken);

            return city;
        }

        private async Task<bool> CheckIfCityWithSameNameExists(
            string name)
            => await this.citiesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
    }
}
