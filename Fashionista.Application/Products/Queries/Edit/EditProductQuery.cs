namespace Fashionista.Application.Products.Queries.Edit
{
    using Fashionista.Application.Products.Commands.Edit;
    using MediatR;

    public class EditProductQuery : IRequest<EditProductCommand>
    {
        public int Id { get; set; }
    }
}
