// ReSharper disable PossibleMultipleEnumeration

namespace Fashionista.Application.Tests.MainCategories.Queries.GetAllMainCategorySelectList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.MainCategories.Queries.GetAllMainCategoriesSelectList;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllMainCategorySelectListTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "GetAllMainCategorySelectList query tests.")]
        [Fact(DisplayName = "Handle given valid request should return IEnumerable.")]
        public async Task Handle_GivenValidRequest_ShouldReturnIEnumerable()
        {
            // Arrange
            await this.deletableEntityRepository.SaveChangesAsync();

            var query = new GetAllMainCategoriesSelectListQuery();
            var sut = new GetAllMainCategoriesSelectListQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeAssignableTo<GetAllMainCategoriesSelectListViewModel>();
            viewModel.MainCategories.Count().ShouldBe(3);
        }

        [Trait(nameof(MainCategory), "GetAllMainCategorySelectList query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllMainCategoriesSelectListQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}