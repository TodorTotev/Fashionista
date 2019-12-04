namespace Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes
{
    using MediatR;

    public class GetColorsAndSizesQuery : IRequest<ProductColorsAndSizesViewModel>
    {
        public int Id { get; set; }
    }
}