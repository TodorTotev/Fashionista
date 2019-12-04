namespace Fashionista.Application.Orders.Queries.Complete
{
    using Fashionista.Application.Orders.Commands.Complete;
    using MediatR;

    public class CompleteOrderQuery : IRequest<CompleteOrderCommand>
    {
    }
}
