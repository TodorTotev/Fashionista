namespace Fashionista.Application.Tests.Orders.Queries.Complete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Orders.Commands.Complete;
    using Fashionista.Application.Orders.Queries.Complete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CompleteOrderQueryTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "CompleteOrder query test")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new CompleteOrderQuery();
            var shoppingCartRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(this.dbContext);
            var sut = new CompleteOrderQueryHandler(
                this.deletableEntityRepository,
                shoppingCartRepository,
                this.userAssistantMock.Object,
                this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            command.ShouldNotBeNull();
            command.ShouldBeOfType<CompleteOrderCommand>();
            command.Recipient.ShouldBe("TestFirstName TestLastName");
            command.DeliveryFee.ShouldBe(7M);
            command.DeliveryAddressName.ShouldBe("TestAddress");
        }

        [Trait(nameof(Order), "CompleteOrder query test")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new CompleteOrderQuery();
            var userMock = new Mock<IUserAssistant>();
            userMock.Setup(x => x.UserId).Returns("asd");

            var shoppingCartRepository = new EfDeletableEntityRepository<ShoppingCartProduct>(this.dbContext);
            var sut = new CompleteOrderQueryHandler(
                this.deletableEntityRepository,
                shoppingCartRepository,
                userMock.Object,
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Order), "CompleteOrder query test")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CompleteOrderQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<ShoppingCartProduct>>(),
                It.IsAny<IUserAssistant>(),
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}