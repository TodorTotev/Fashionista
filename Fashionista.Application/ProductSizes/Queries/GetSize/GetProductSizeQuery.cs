namespace Fashionista.Application.ProductSizes.Queries.GetSize
{
    using Fashionista.Application.Common.Models;
    using MediatR;

    public class GetProductSizeQuery : IRequest<ProductSizeLookupModel>
    {
        public int Id { get; set; }
    }
}
