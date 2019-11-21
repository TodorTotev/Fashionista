using Fashionista.Application.Exceptions;

namespace Fashionista.Application.Tests.ProductSizes.Queries.GetProductSize
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ProductSizes.Queries.GetSize;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetProductSizeQueryTests : BaseTest<ProductSize>
    {
        [Trait(nameof(ProductSize), "GetProductSize query tests")]
        [Fact(DisplayName = "Handle given valid request should return size")]
        public async Task Handle_GivenValidRequest_ShouldReturnSize()
        {
            // Arrange
            var query = new GetProductSizeQuery { Id = 1 };
            var sut = new GetProductSizeQueryHandler(this.deletableEntityRepository);

            // Act
            var size = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            size.ShouldNotBeNull();
            size.Name.ShouldBe("TestSize");
        }

        [Trait(nameof(ProductSize), "GetProductSize query tests")]
        [Fact(DisplayName = "Handle given valid request should return ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetProductSizeQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductSize), "GetProductSize query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetProductSizeQuery { Id = 1000 };
            var sut = new GetProductSizeQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

    }
}