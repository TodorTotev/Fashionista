namespace Fashionista.Application.ProductColors.Commands.Create
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductColorCommand : IRequest<int>, IMapTo<ProductColor>
    {
        public string Name { get; set; }

        public int ProductId { get; set; }
    }
}
