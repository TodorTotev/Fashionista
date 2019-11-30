namespace Fashionista.Application.Products.Queries.Details
{
    using MediatR;

    public class GetProductDetailsQuery : IRequest<GetProductDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
