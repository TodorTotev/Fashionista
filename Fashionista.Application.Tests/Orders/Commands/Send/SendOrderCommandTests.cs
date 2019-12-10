using System;
using Fashionista.Application.Exceptions;

namespace Fashionista.Application.Tests.Orders.Commands.Send
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Orders.Commands.SendOrder;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Moq;
    using Shouldly;
    using Xunit;

    public class SendOrderCommandTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "SendOrder command test")]
        [Fact(DisplayName = "Handle given valid request should mark order sent")]
        public async Task Handle_GivenValidRequest_ShouldMarkOrderSent()
        {
            // Arrange
            var command = new SendOrderCommand { Id = 2 };
            var sut = new SendOrderCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var order = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(2);

            order.OrderState.ShouldBe(OrderState.Delivered);
        }

        [Trait(nameof(Order), "SendOrder command test")]
        [Fact(DisplayName = "Handle given invalid request throw OrderAlreadySentException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowOrderAlreadySentException()
        {
            // Arrange
            var command = new SendOrderCommand { Id = 2 };
            var sut = new SendOrderCommandHandler(this.deletableEntityRepository);

            var order = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(2);

            order.OrderState = OrderState.Delivered;
            this.deletableEntityRepository.Update(order);
            await this.deletableEntityRepository.SaveChangesAsync();

            // Act & Assert
            await Should.ThrowAsync<OrderAlreadySentException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Order), "SendOrder command test")]
        [Fact(DisplayName = "Handle given invalid request throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new SendOrderCommand { Id = 50 };
            var sut = new SendOrderCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Order), "SendOrder command test")]
        [Fact(DisplayName = "Handle given null request throw ArgumentNullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new SendOrderCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}