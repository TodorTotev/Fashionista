// ReSharper disable PossibleNullReferenceException

using System;
using Fashionista.Application.Exceptions;

namespace Fashionista.Application.Tests.Orders.Commands.Cancel
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Orders.Commands.Cancel;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CancelOrderCommandTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "CreateOrder command tests")]
        [Fact(DisplayName = "Handle given valid request should return order id")]
        public async Task Handle_GivenValidRequest_ShouldReturnOrderId()
        {
            // Arrange
            var command = new CancelOrderCommand { Id = 2 };
            var sut = new CancelOrderCommandHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var order = this.dbContext.Orders.FirstOrDefault(x => x.Id == id);
            order.IsDeleted.ShouldBe(true);
            order.OrderState.ShouldBe(OrderState.Cancelled);
            order.PaymentState.ShouldBe(PaymentState.Expired);
        }

        [Trait(nameof(Order), "CreateOrder command tests")]
        [Fact(DisplayName = "Handle given valid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CancelOrderCommand { Id = 1 };
            var sut = new CancelOrderCommandHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Order), "CreateOrder command tests")]
        [Fact(DisplayName = "Handle given valid request should throw NotFouArgumentNullExceptionndException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CancelOrderCommandHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
     }
}