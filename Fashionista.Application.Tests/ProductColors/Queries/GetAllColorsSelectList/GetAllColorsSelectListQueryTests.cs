namespace Fashionista.Application.Tests.ProductColors.Queries.GetAllColorsSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ProductColors.Queries.GetAllColorsSelectList;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllColorsSelectListQueryTests : BaseTest<ProductColor>
    {
        [Trait(nameof(ProductColor), "GetAllColorsSelectList query tests")]
        [Fact(DisplayName = "Handle given valid request should return viewmodel")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllColorsSelectListQuery();
            var sut = new GetAllColorsSelectListQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.AllColors.ShouldNotBeNull();
            viewModel.AllColors.Count().ShouldBe(1);
            viewModel.ShouldBeOfType<GetAllColorsSelectListViewModel>();
        }

        [Trait(nameof(ProductColor), "GetAllColorsSelectList query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllColorsSelectListQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}