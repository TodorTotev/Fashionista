namespace Fashionista.Application.ProductAttributes.Queries.GetAll
{
    using MediatR;

    public class GetAllProductAttributesQuery : IRequest<GetAllProductAttributesViewModel>
    {
        public int Id { get; set; }
    }
}