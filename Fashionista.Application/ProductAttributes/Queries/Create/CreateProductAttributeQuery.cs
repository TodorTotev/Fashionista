namespace Fashionista.Application.ProductAttributes.Queries.Create
{
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using MediatR;

    public class CreateProductAttributeQuery : IRequest<CreateProductAttributeCommand>
    {
        public int Id { get; set; }
    }
}
