// ReSharper disable PossibleNullReferenceException

using System;
using Fashionista.Application.Exceptions;
using Fashionista.Application.Interfaces;

namespace Fashionista.Application.Tests.Orders.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Orders.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateOrderCommandTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "CreateOrder command tests")]
        [Fact(DisplayName = "Handle given valid request should return order id")]
        public async Task Handle_GivenValidRequest_ShouldReturnOrderId()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                DeliveryAddressId = 1,
                DeliveryFee = 100,
            };

            var addressRepository = new EfDeletableEntityRepository<Address>(this.dbContext);

            var sut = new CreateOrderCommandHandler(
                this.deletableEntityRepository,
                addressRepository,
                this.userAssistantMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            var order = await this.dbContext.Orders
                .SingleOrDefaultAsync(x => x.Id == id);

            order.ShouldNotBeNull();
            order.ShouldBeOfType<Order>();
            order.DeliveryAddressId.ShouldBe(1);
            order.DeliveryFee.ShouldBe(100);
            order.RecipientPhoneNumber.ShouldBe("12345678");
            order.Recipient.ShouldBe("TestFirstName TestLastName");
        }

        [Trait(nameof(Order), "CreateOrder command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                DeliveryAddressId = 1000,
                DeliveryFee = 100,
            };

            var addressRepository = new EfDeletableEntityRepository<Address>(this.dbContext);

            var sut = new CreateOrderCommandHandler(
                this.deletableEntityRepository,
                addressRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Order), "CreateOrder command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException(")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateOrderCommandHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<Address>>(),
                It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}