namespace Fashionista.Application.Tests.Orders.Queries.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Orders.Commands.Create;
    using Fashionista.Application.Orders.Queries.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateOrderQueryTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "CreateOrder query tests")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new CreateOrderQuery();
            var addressesRepository = new EfDeletableEntityRepository<Address>(this.dbContext);
            var sut = new CreateOrderQueryHandler(this.userAssistantMock.Object, addressesRepository);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeOfType<CreateOrderCommand>();
            command.CustomerInformation.ShouldBe("TestFirstName TestLastName");
        }

        [Trait(nameof(Order), "CreateOrder query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var addressesRepository = new EfDeletableEntityRepository<Address>(this.dbContext);
            var sut = new CreateOrderQueryHandler(this.userAssistantMock.Object, addressesRepository);

            // Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}