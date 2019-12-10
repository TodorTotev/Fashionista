namespace Fashionista.Application.Orders.Commands.SendOrder
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

    public class SendOrderCommandHandler : IRequestHandler<SendOrderCommand, int>
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public SendOrderCommandHandler(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<int> Handle(SendOrderCommand request, CancellationToken cancellationToken)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            var entity = await this.ordersRepository
                             .All()
                             .Where(x => x.Id == request.Id)
                             .SingleOrDefaultAsync(cancellationToken)
                         ?? throw new NotFoundException(nameof(Order), request.Id);

            if (entity.OrderState == OrderState.Delivered)
            {
                throw new OrderAlreadySentException();
            }

            entity.OrderState = OrderState.Delivered;
            entity.DateSent = DateTime.UtcNow;

            this.ordersRepository.Update(entity);
            await this.ordersRepository.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
