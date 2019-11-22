namespace Fashionista.Application.Tests.ProductSizes.Queries.GetAllSizesSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductSizes.Queries.GetAllSizesSelectList;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllSizesSelectListQueryTests : BaseTest<ProductSize>
    {
        [Trait(nameof(ProductSize), "GetAllProductSizesSelectList query tests")]
        [Fact(DisplayName = "Handle given valid request should return viewmodel")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllSizesSelectListQuery { MainCategoryId = 1 };
            var sut = new GetAllSizesSelectListQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllSizesSelectListViewModel>();
            viewModel.AllSizes.Count().ShouldBe(1);
        }

        [Trait(nameof(ProductSize), "GetAllProductSizesSelectList query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetAllSizesSelectListQuery { MainCategoryId = 100 };
            var sut = new GetAllSizesSelectListQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductSize), "GetAllProductSizesSelectList query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllSizesSelectListQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}