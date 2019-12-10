namespace Fashionista.Application.Tests.Orders.Queries.GetProcessed
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Orders.Queries.GetProcessed;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetProcessedOrdersQueryTests : BaseTest<Order>
    {
        [Trait(nameof(Order), "GetProcessedOrders query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetProcessedOrdersPagedQuery();
            var sut = new GetProcessedOrdersPagedQueryHandler(this.deletableEntityRepository);

            // Act
            var model = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            model.ShouldNotBeNull();
            model.ShouldBeOfType<OrdersViewModel>();
        }

        [Trait(nameof(Order), "GetProcessedOrders query tests")]
        [Fact(DisplayName = "Handle given null request should return ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldReturnArgumentNullException()
        {
            // Arrange
            var sut = new GetProcessedOrdersPagedQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}