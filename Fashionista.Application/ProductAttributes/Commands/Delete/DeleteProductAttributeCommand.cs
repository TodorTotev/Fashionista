namespace Fashionista.Application.ProductAttributes.Commands.Delete
{
    using MediatR;

    public class DeleteProductAttributeCommand : IRequest<int>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
    }
}
