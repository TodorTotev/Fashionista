namespace Fashionista.Application.Orders.Commands.Cancel
{
    using MediatR;

    public class CancelOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
