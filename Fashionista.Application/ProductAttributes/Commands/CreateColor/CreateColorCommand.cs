namespace Fashionista.Application.ProductAttributes.Commands.CreateColor
{
    using MediatR;

    public class CreateColorCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
