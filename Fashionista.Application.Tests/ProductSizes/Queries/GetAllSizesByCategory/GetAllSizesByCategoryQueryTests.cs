namespace Fashionista.Application.Tests.ProductSizes.Queries.GetAllSizesByCategory
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductSizes.Queries.GetAllSizesByCategory;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllSizesByCategoryQueryTests : BaseTest<ProductSize>
    {
        [Trait(nameof(ProductSize), "GetAllSizesByCategory query tests")]
        [Fact(DisplayName = "Handle given valid request should return list of sizes")]
        public async Task Handle_GivenValidRequest_ShouldReturnListOfSizes()
        {
            // Arrange
            var query = new GetAllSizesByCategoryQuery { Id = 1 };
            var categoryRepository = new EfDeletableEntityRepository<MainCategory>(this.dbContext);
            var sut = new GetAllSizesByCategoryQueryHandler(
                this.deletableEntityRepository, categoryRepository);

            // Act
            var list = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            list.ShouldNotBeNull();
            list.Count.ShouldBe(1);
        }

        [Trait(nameof(ProductSize), "GetAllSizesByCategory query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetAllSizesByCategoryQuery { Id = 1000 };
            var categoryRepository = new EfDeletableEntityRepository<MainCategory>(this.dbContext);
            var sut = new GetAllSizesByCategoryQueryHandler(
                this.deletableEntityRepository, categoryRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductSize), "GetAllSizesByCategory query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenInvalidProductId_ShouldArgumentNullException()
        {
            // Arrange
            var sut = new GetAllSizesByCategoryQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<MainCategory>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}