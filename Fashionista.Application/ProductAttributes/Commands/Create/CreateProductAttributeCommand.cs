namespace Fashionista.Application.ProductAttributes.Commands.Create
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductAttributeCommand : IRequest<ProductAttributes>, IMapTo<ProductAttributes>
    {
        public string ProductName { get; set; }

        public int ProductColorId { get; set; }

        public int ProductSizeId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
