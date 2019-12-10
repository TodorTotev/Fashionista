using System.Collections.Generic;

namespace Fashionista.Application.Orders.Queries.GetProcessed
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetProcessedOrdersQueryHandler : IRequestHandler<GetProcessedOrdersQuery, OrdersViewModel>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public GetProcessedOrdersQueryHandler(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<OrdersViewModel> Handle(GetProcessedOrdersQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var orders = new List<OrderLookupModel>();

            if (request.IsDelivered)
            {
                orders = await this.ordersRepository
                    .All()
                    .Where(x => x.OrderState == OrderState.Processed
                                && x.PaymentState == PaymentState.Paid)
                    .To<OrderLookupModel>()
                    .ToListAsync(cancellationToken);
            }
            else
            {
                orders = await this.ordersRepository
                    .All()
                    .Where(x => x.OrderState == OrderState.Delivered
                                && x.PaymentState == PaymentState.Paid)
                    .To<OrderLookupModel>()
                    .ToListAsync(cancellationToken);
            }

            var viewModel = new OrdersViewModel { CurrentOrders = orders };
            return viewModel;
        }
    }
}
