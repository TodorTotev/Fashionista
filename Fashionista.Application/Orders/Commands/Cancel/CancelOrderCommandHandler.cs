namespace Fashionista.Application.Orders.Commands.Cancel
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, int>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IUserAssistant userAssistant;

        public CancelOrderCommandHandler(IDeletableEntityRepository<Order> ordersRepository, IUserAssistant userAssistant)
        {
            this.ordersRepository = ordersRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var order = await this.ordersRepository
                            .All()
                            .Where(x => x.ApplicationUserId == this.userAssistant.UserId
                                        && x.PaymentState == PaymentState.AwaitingPayment
                                        && x.Id == request.Id
                                        && x.OrderState == OrderState.Processed)
                            .SingleOrDefaultAsync(cancellationToken)
                        ?? throw new NotFoundException(nameof(Order), request.Id);

            order.OrderState = OrderState.Cancelled;
            order.PaymentState = PaymentState.Expired;
            order.IsDeleted = true;

            await this.ordersRepository.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
