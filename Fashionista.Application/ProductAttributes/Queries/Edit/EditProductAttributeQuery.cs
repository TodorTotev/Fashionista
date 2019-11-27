namespace Fashionista.Application.ProductAttributes.Queries.Edit
{
    using Fashionista.Application.ProductAttributes.Commands.Edit;
    using MediatR;

    public class EditProductAttributeQuery : IRequest<EditProductAttributeCommand>
    {
        public int Id { get; set; }
    }
}
