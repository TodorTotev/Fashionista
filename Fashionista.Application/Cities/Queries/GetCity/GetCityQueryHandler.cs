namespace Fashionista.Application.Cities.Queries.GetCity
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetCityQueryHandler : IRequestHandler<GetCityQuery, City>
    {
        private readonly IDeletableEntityRepository<City> citiesRepository;

        public GetCityQueryHandler(IDeletableEntityRepository<City> citiesRepository)
        {
            this.citiesRepository = citiesRepository;
        }

        public async Task<City> Handle(GetCityQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            if (!await CommonCheckAssistant.CheckIfCityWithSameNameExists(request.Name, this.citiesRepository))
            {
                throw new NotFoundException(nameof(City), request.Name);
            }

            return await this.citiesRepository
                .AllAsNoTracking()
                .Where(x => x.Name == request.Name)
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
