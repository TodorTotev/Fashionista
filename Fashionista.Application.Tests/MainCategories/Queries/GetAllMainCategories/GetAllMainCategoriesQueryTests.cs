using Fashionista.Application.SubCategories.Queries;
using Fashionista.Application.SubCategories.Queries.GetAllSubCategories;

namespace Fashionista.Application.Tests.MainCategories.Queries.GetAllMainCategories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategories;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllMainCategoriesQueryTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "GetAllMainCategories query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllMainCategoriesQuery();
            var sut = new GetAllMainCategoriesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.MainCategories.Count().ShouldBe(3);
            viewModel.ShouldBeOfType<GetAllMainCategoriesViewModel>();
        }

        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllMainCategoriesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}