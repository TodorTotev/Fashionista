namespace Fashionista.Application.Orders.Queries.Create
{
    using Fashionista.Application.Orders.Commands.Create;
    using MediatR;

    public class CreateOrderQuery : IRequest<CreateOrderCommand>
    {
    }
}
