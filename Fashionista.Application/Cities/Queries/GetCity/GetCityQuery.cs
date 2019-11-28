namespace Fashionista.Application.Cities.Queries.GetCity
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetCityQuery : IRequest<City>
    {
        public string Name { get; set; }
    }
}
