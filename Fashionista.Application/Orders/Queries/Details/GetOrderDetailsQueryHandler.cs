namespace Fashionista.Application.Orders.Queries.Details
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IUserAssistant userAssistant;

        public GetOrderDetailsQueryHandler(
            IDeletableEntityRepository<Order> ordersRepository,
            IUserAssistant userAssistant)
        {
            this.ordersRepository = ordersRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<OrderDetailsViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException();

            var order = await this.ordersRepository
                            .All()
                            .Where(x => x.ApplicationUserId == this.userAssistant.UserId
                                        && x.PaymentState == PaymentState.AwaitingPayment
                                        && x.Id == request.Id
                                        && x.OrderState == OrderState.Processed)
                            .To<OrderLookupModel>()
                            .SingleOrDefaultAsync(cancellationToken)
                        ?? throw new NotFoundException(nameof(Order), request.Id);

            return new OrderDetailsViewModel
            {
                Order = order,
            };
        }
    }
}
