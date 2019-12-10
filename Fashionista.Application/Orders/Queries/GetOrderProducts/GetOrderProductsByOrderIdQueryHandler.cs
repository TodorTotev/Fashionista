namespace Fashionista.Application.Orders.Queries.GetOrderProducts
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
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetOrderProductsByOrderIdQueryHandler :
        IRequestHandler<GetOrderProductsByOrderIdQuery, OrderProductsViewModel>
    {
        private readonly IDeletableEntityRepository<OrderProduct> orderProductsRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public GetOrderProductsByOrderIdQueryHandler(
            IDeletableEntityRepository<OrderProduct> orderProductsRepository,
            IDeletableEntityRepository<Order> ordersRepository)
        {
            this.orderProductsRepository = orderProductsRepository;
            this.ordersRepository = ordersRepository;
        }

        public async Task<OrderProductsViewModel> Handle(GetOrderProductsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfOrderExists(request.Id))
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            var products = await this.orderProductsRepository
                .All()
                .Where(x => x.OrderId == request.Id)
                .To<OrderProductLookupModel>()
                .ToListAsync(cancellationToken);

            return new OrderProductsViewModel
            {
                Products = products,
            };
        }

        private async Task<bool> CheckIfOrderExists(int id) =>
            await this.ordersRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
    }
}
