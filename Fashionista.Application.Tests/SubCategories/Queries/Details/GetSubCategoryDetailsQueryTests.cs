namespace Fashionista.Application.Tests.SubCategories.Queries.Details
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.SubCategories.Queries.Details;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetSubCategoryDetailsQueryTests : BaseTest<SubCategory>
    {
        [Trait(nameof(SubCategory), "GetSubCategoryDetails query tests")]
        [Fact(DisplayName = "Handle given valid request should return subcategory")]
        public async Task Handle_GivenValidRequest_ShouldReturnSubCategory()
        {
            // Arrange
            var query = new GetSubCategoryDetailsQuery { Id = 1 };
            var sut = new GetSubCategoryDetailsQueryHandler(this.deletableEntityRepository);

            // Act
            var category = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            category.ShouldNotBeNull();
            category.ShouldBeOfType<SubCategoryLookupModel>();
            category.Name.ShouldBe("Category1");
        }

        [Trait(nameof(SubCategory), "GetSubCategoryDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetSubCategoryDetailsQuery { Id = 100 };
            var sut = new GetSubCategoryDetailsQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(SubCategory), "GetSubCategoryDetails query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetSubCategoryDetailsQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}