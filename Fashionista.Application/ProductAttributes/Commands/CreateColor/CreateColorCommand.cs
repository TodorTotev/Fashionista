namespace Fashionista.Application.ProductAttributes.Commands.CreateColor
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateColorCommand : IRequest<int>, IMapTo<ProductColor>
    {
        public string Name { get; set; }
    }
}
