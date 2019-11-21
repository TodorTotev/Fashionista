using Fashionista.Domain.Entities;
using MediatR;

namespace Fashionista.Application.ProductSizes.Queries.GetSize
{
    public class GetProductSizeQuery : IRequest<ProductSize>
    {
        public string Name { get; set; }
    }
}