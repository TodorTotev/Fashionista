namespace Fashionista.Application.Brands.Commands.Create
{
    using System.Collections.Generic;

    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreateBrandCommand : IRequest<int>, IMapFrom<Brand>
    {
        public string Name { get; set; }

        public IFormFile Photo { get; set; }
    }
}