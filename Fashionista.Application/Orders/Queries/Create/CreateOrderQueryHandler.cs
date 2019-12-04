#pragma warning disable 1998

namespace Fashionista.Application.Orders.Queries.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Orders.Commands.Create;
    using MediatR;

    public class CreateOrderQueryHandler : IRequestHandler<CreateOrderQuery, CreateOrderCommand>
    {
        private readonly IUserAssistant userAssistant;

        public CreateOrderQueryHandler(IUserAssistant userAssistant)
        {
            this.userAssistant = userAssistant;
        }

        public async Task<CreateOrderCommand> Handle(CreateOrderQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            return new CreateOrderCommand
            {
                CustomerInformation = this.userAssistant.FullName,
            };
        }
    }
}
