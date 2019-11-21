namespace Fashionista.Application.ProductAttributes.Commands.Create
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductAttributeCommand : IRequest<int>, IMapTo<ProductAttributes>
    {
        public int ProductColorId { get; set; }

        public int ProductSizeId { get; set; }

        public int Quantity { get; set; }
    }
}
