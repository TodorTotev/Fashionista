namespace Fashionista.Application.ProductSizes.Commands.Create
{
    using Fashionista.Application.Interfaces.Mapping;
    using Fashionista.Domain.Entities;
    using MediatR;

    public class CreateProductSizeCommand : IRequest<int>, IMapTo<ProductSize>
    {
        public string Name { get; set; }

        public int MainCategoryId { get; set; }
    }
}
