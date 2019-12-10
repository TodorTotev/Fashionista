namespace Fashionista.Application.Tests.SubCategories.Queries.GetAllSubCategories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.SubCategories.Queries.GetAllSubCategories;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllSubCategoriesQueryTests : BaseTest<SubCategory>
    {
        [Trait(nameof(SubCategory), "GetAllSubCategories query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllSubCategoriesQuery();
            var sut = new GetAllSubCategoriesQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.SubCategories.Count().ShouldBe(3);
            viewModel.ShouldBeOfType<GetAllSubCategoriesViewModel>();
        }

        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllSubCategoriesQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}