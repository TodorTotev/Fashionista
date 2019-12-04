namespace Fashionista.Application.Tests.Products.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Products.Queries.Details;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetProductDetailsQueryTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "GetProductDetails query tests")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new GetProductDetailsQuery { Id = 1 };
            var attributesRepository = new EfDeletableEntityRepository<ProductAttributes>(this.dbContext);
            var sut = new GetProductDetailsQueryHandler(
                this.deletableEntityRepository,
                attributesRepository,
                this.mapper);

            // Act
            var product = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            product.ShouldNotBeNull();
            product.ShouldBeOfType<GetProductDetailsViewModel>();
            product.Product.Name.ShouldBe("ActiveProduct");
        }

        [Trait(nameof(Product), "GetProductDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should return NotFoundException ")]
        public async Task Handle_GivenInvalidRequest_ShouldReturnNotFoundException()
        {
            // Arrange
            var query = new GetProductDetailsQuery { Id = 1000 };
            var attributesRepository = new EfDeletableEntityRepository<ProductAttributes>(this.dbContext);
            var sut = new GetProductDetailsQueryHandler(
                this.deletableEntityRepository,
                attributesRepository,
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "GetProductDetails query tests")]
        [Fact(DisplayName = "Handle given null request should return ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldReturnArgumentNullException()
        {
            // Arrange
            var sut = new GetProductDetailsQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<ProductAttributes>>(),
                this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}