using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fashionista.Application.Common.Models;

namespace Fashionista.Application.Orders.Commands.Complete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, int>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IUserAssistant userAssistant;
        private readonly IMapper mapper;

        public CompleteOrderCommandHandler(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IUserAssistant userAssistant,
            IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.userAssistant = userAssistant;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var order = await this.ordersRepository
                .All()
                .Where(x => x.ApplicationUserId == this.userAssistant.UserId
                            && x.OrderState == OrderState.Processing)
                .SingleOrDefaultAsync(cancellationToken);

            var cartProducts = await this.shoppingCartProductsRepository
                .All()
                .Where(x => x.ShoppingCartId == this.userAssistant.ShoppingCartId)
                .ProjectTo<OrderProductLookupModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            order.OrderProducts = cartProducts
                .Select(currentProduct => new OrderProduct
                {
                    Order = order,
                    ProductId = currentProduct.ProductId,
                    Quantity = currentProduct.Quantity,
                    Price = currentProduct.ProductPrice,
                }).ToList();

            order.TotalPrice = order.OrderProducts.Sum(x => x.Quantity * x.Price) + request.DeliveryFee;
            order.OrderDate = DateTime.UtcNow;
            order.InvoiceNumber = order.Id.ToString().PadLeft(5, '0');
            order.PaymentType = request.PaymentType;
            order.OrderState = OrderState.Processed;
            order.PaymentState = PaymentState.AwaitingPayment;

            this.ordersRepository.Update(order);
            await this.ordersRepository.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
