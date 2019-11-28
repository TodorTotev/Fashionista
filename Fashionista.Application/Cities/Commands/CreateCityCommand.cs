namespace Fashionista.Application.Cities.Commands
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateCityCommand : IRequest<City>, IMapTo<City>
    {
        public string Name { get; set; }

        public string Postcode { get; set; }
    }
}
