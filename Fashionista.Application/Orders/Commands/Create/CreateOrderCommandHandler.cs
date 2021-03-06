namespace Fashionista.Application.Orders.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<Address> addressesRepository;
        private readonly IUserAssistant userAssistant;

        public CreateOrderCommandHandler(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<Address> addressesRepository,
            IUserAssistant userAssistant)
        {
            this.ordersRepository = ordersRepository;
            this.addressesRepository = addressesRepository;
            this.userAssistant = userAssistant;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (!await this.CheckIfAddressExists(request.DeliveryAddressId))
            {
                throw new NotFoundException(nameof(Address), request.DeliveryAddressId);
            }

            var order = new Order
            {
                OrderState = OrderState.Pending,
                ApplicationUserId = this.userAssistant.UserId,
                DeliveryAddressId = request.DeliveryAddressId,
                Recipient = request.CustomerInformation,
                RecipientPhoneNumber = request.PhoneNumber,
                DeliveryFee = request.DeliveryFee,
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync(cancellationToken);

            return order.Id;
        }

        private async Task<bool> CheckIfAddressExists(int? id) =>
            await this.addressesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Id == id);
    }
}
