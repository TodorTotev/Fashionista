namespace Fashionista.Application.Tests.Orders.Commands.Complete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Orders.Commands.Complete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CompleteOrderCommandTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "CompleteOrder command tests")]
        [Fact(DisplayName = "Handle given valid request should return order id")]
        public async Task Handle_GivenValidRequest_ShouldReturnOrderId()
        {
            // Arrange
            var shoppingCartRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(this.dbContext);
            var command = new CompleteOrderCommand { PaymentType = PaymentType.PayPal, DeliveryFee = 7, };
            var sut = new CompleteOrderCommandHandler(
                this.deletableEntityRepository,
                shoppingCartRepository,
                this.userAssistantMock.Object,
                this.mapper);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var order = this.dbContext.Orders.FirstOrDefault(x => x.Id == id);
            order.ShouldNotBeNull();
            order.Recipient.ShouldBe(this.userAssistantMock.Object.FullName);
            order.DeliveryFee.ShouldBe(7);
            order.OrderState.ShouldBe(OrderState.Processed);
            order.PaymentState.ShouldBe(PaymentState.AwaitingPayment);
            order.OrderProducts.Count.ShouldBe(1);
        }

        [Trait(nameof(Order), "CompleteOrder command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CompleteOrderCommandHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<ShoppingCartProduct>>(),
                this.userAssistantMock.Object,
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}