// ReSharper disable RedundantAssignment
namespace Fashionista.Application.Orders.Queries.GetProcessed
{
    using System;
    using System.Collections.Generic;
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

    public class GetProcessedOrdersPagedQueryHandler : IRequestHandler<GetProcessedOrdersPagedQuery, OrdersViewModel>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public GetProcessedOrdersPagedQueryHandler(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<OrdersViewModel> Handle(GetProcessedOrdersPagedQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var orders = new List<OrderLookupModel>();

            if (!request.IsDelivered)
            {
                orders = await this.ordersRepository
                    .All()
                    .Where(x => x.OrderState == OrderState.Processed
                                && x.PaymentState == PaymentState.Paid)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .To<OrderLookupModel>()
                    .ToListAsync(cancellationToken);
            }
            else
            {
                orders = await this.ordersRepository
                    .All()
                    .Where(x => x.OrderState == OrderState.Delivered
                                && x.PaymentState == PaymentState.Paid)
                    .Skip(request.PageNumber * request.PageSize)
                    .Take(request.PageSize)
                    .To<OrderLookupModel>()
                    .ToListAsync(cancellationToken);
            }

            var viewModel = new OrdersViewModel { CurrentOrders = orders };
            return viewModel;
        }
    }
}
