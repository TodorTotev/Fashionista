namespace Fashionista.Application.Tests.Brands.Queries.GetAllBrandsSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Brands.Queries.GetAllBrandsSelectList;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllBrandsSelectListTests : BaseTest<Brand>
    {
        [Trait(nameof(Brand), "GetAllBrandsSelectList query tests")]
        [Fact(DisplayName = "Handle given valid request should return IEnumerable")]
        public async Task Handle_GivenValidRequest_ShouldReturnIEnumerable()
        {
            // Arrange
            var query = new GetAllBrandsSelectListQuery();
            var sut = new GetAllBrandsSelectListQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllBrandsSelectListViewModel>();
            viewModel.Brands.Count().ShouldBe(1);
        }

        [Trait(nameof(Brand), "GetAllBrandsSelectList query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllBrandsSelectListQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}