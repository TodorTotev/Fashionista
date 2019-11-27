namespace Fashionista.Application.ProductAttributes.Commands.Edit
{
    using MediatR;

    public class EditProductAttributeCommand : IRequest<int>
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}