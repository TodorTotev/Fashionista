namespace Fashionista.Application.Tests.Orders.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Orders.Queries.Details;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetOrderDetailsQueryTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "GetOrderDetails query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetOrderDetailsQuery { Id = 2 };
            var sut = new GetOrderDetailsQueryHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object,
                this.mapper);

            // Act
            var model = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            model.ShouldNotBeNull();
            model.ShouldBeOfType<OrderDetailsViewModel>();
            model.Order.ShouldNotBeNull();
            model.Order.Id.ShouldBe(2);
        }

        [Trait(nameof(Order), "GetOrderDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetOrderDetailsQuery { Id = 100 };
            var sut = new GetOrderDetailsQueryHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object,
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Order), "GetOrderDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetOrderDetailsQueryHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object,
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}