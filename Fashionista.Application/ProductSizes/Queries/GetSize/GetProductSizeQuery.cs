namespace Fashionista.Application.ProductSizes.Queries.GetSize
{
    using Fashionista.Domain.Entities;
    using MediatR;

    public class GetProductSizeQuery : IRequest<ProductSize>
    {
        public int Id { get; set; }
    }
}
