// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Products.Queries.GetAllProductsByCategory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Products.Queries.GetAllProductsByCategory;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllProductsByCategoryQueryTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "GetAllProductsByCategory query tests")]
        [Fact(DisplayName = "Handle given valid request should return list of products")]
        public async Task Handle_GivenValidRequest_ShouldReturnListOfProducts()
        {
            // Arrange
            var query = new GetAllProductsByCategoryQuery { Id = 1 };
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(this.dbContext);

            var sut = new GetAllProductsByCategoryQueryHandler(
                this.deletableEntityRepository,
                subCategoryRepository);

            // Act
            var list = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            list.ShouldNotBeNull();
            list.ShouldBeOfType<List<ProductLookupModel>>();
            list.Count.ShouldBe(1);
            list.FirstOrDefault().SubCategoryId.ShouldBe(1);
            list.FirstOrDefault().Brand.Id.ShouldBe(1);
        }

        [Trait(nameof(Product), "GetAllProductsByCategory query tests")]
        [Fact(DisplayName = "Handle given invalid category id should throw NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetAllProductsByCategoryQuery { Id = 100 };
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(this.dbContext);

            var sut = new GetAllProductsByCategoryQueryHandler(
                this.deletableEntityRepository,
                subCategoryRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "GetAllProductsByCategory query tests")]
        [Fact(DisplayName = "Handle given invalid category id should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllProductsByCategoryQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<SubCategory>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}