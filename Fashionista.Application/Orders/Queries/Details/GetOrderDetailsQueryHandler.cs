namespace Fashionista.Application.Orders.Queries.Details
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsViewModel>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IUserAssistant userAssistant;
        private readonly IMapper mapper;

        public GetOrderDetailsQueryHandler(
            IDeletableEntityRepository<Order> ordersRepository,
            IUserAssistant userAssistant,
            IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.userAssistant = userAssistant;
            this.mapper = mapper;
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
                            .ProjectTo<OrderLookupModel>(this.mapper.ConfigurationProvider)
                            .SingleOrDefaultAsync(cancellationToken)
                        ?? throw new NotFoundException(nameof(Order), request.Id);

            return new OrderDetailsViewModel
            {
                Order = order,
            };
        }
    }
}
