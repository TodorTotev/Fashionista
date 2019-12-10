namespace Fashionista.Application.Orders.Queries.Complete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Infrastructure.Automapper;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Orders.Commands.Complete;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CompleteOrderQueryHandler : IRequestHandler<CompleteOrderQuery, CompleteOrderCommand>
    {
        private readonly IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IUserAssistant userAssistant;

        public CompleteOrderQueryHandler(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<ShoppingCartProduct> shoppingCartProductsRepository,
            IUserAssistant userAssistant)
        {
            this.ordersRepository = ordersRepository;
            this.shoppingCartProductsRepository = shoppingCartProductsRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<CompleteOrderCommand> Handle(
            CompleteOrderQuery request,
            CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfCartContainsProducts(this.userAssistant.UserId))
            {
                throw new CartAlreadyContainsProductException();
            }

            var command = await this.ordersRepository
                            .AllAsNoTracking()
                            .Where(x => x.ApplicationUserId == this.userAssistant.UserId
                                        && x.OrderState == OrderState.Pending)
                            .To<CompleteOrderCommand>()
                            .SingleOrDefaultAsync(cancellationToken)
                        ?? throw new NotFoundException(nameof(ApplicationUser), $"doesn't have processing orders");

            var totalPrice = this.shoppingCartProductsRepository
                .AllAsNoTracking()
                .Where(x => x.ShoppingCart.User.Id == this.userAssistant.UserId)
                .Sum(x => x.Product.Price);

            command.TotalPrice = totalPrice;

            return command;
        }

        private async Task<bool> CheckIfCartContainsProducts(string id) =>
            await this.shoppingCartProductsRepository
                .All()
                .AnyAsync(x => x.ShoppingCart.User.Id == id);
    }
}
