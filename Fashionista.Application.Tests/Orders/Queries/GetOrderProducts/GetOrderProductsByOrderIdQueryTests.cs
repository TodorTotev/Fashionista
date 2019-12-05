namespace Fashionista.Application.Tests.Orders.Queries.GetOrderProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Orders.Queries.GetOrderProducts;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetOrderProductsByOrderIdQueryTests : BaseTest<OrderProduct>
    {
        [Trait(nameof(OrderProduct), "GetOrderProductsByOrderId query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetOrderProductsByOrderIdQuery { Id = 1 };
            var ordersRepository = new EfDeletableEntityRepository<Order>(this.dbContext);
            var sut = new GetOrderProductsByOrderIdQueryHandler(
                this.deletableEntityRepository,
                ordersRepository,
                this.mapper);

            // Act
            var model = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            model.ShouldNotBeNull();
            model.ShouldBeOfType<OrderProductsViewModel>();
            model.Products.Count().ShouldBe(1);
        }

        [Trait(nameof(OrderProduct), "GetOrderProductsByOrderId query tests")]
        [Fact(DisplayName = "Handle given invalid request should return throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetOrderProductsByOrderIdQuery { Id = 5 };
            var ordersRepository = new EfDeletableEntityRepository<Order>(this.dbContext);
            var sut = new GetOrderProductsByOrderIdQueryHandler(
                this.deletableEntityRepository,
                ordersRepository,
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(OrderProduct), "GetOrderProductsByOrderId query tests")]
        [Fact(DisplayName = "Handle given null request should return throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetOrderProductsByOrderIdQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<Order>>(),
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}