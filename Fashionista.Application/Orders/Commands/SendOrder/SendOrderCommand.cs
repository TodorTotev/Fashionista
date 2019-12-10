namespace Fashionista.Application.Orders.Commands.SendOrder
{
    using MediatR;

    public class SendOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
