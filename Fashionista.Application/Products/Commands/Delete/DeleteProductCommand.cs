namespace Fashionista.Application.Products.Commands.Delete
{
    using MediatR;

    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
