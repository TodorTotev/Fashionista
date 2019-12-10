namespace Fashionista.Application.Tests.SubCategories.Queries.GetAllSubCategoriesSelectList
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategoriesSelectList;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllSubCategoriesSelectListTests : BaseTest<SubCategory>
    {
        [Trait(nameof(SubCategory), "GetAllSubCategoriesSelectList query tests")]
        [Fact(DisplayName = "Handle given valid request should return IEnumerable")]
        public async Task Handle_GivenValidRequest_ShouldReturnIEnumerable()
        {
            // Arrange
            var query = new GetAllSubCategoriesSelectListQuery();
            var sut = new GetAllSubCategoriesSelectListQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllSubCategoriesSelectListViewModel>();
            viewModel.SubCategories.Count().ShouldBe(3);
        }

        [Trait(nameof(SubCategory), "GetAllSubCategoriesSelectList query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllSubCategoriesSelectListQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}