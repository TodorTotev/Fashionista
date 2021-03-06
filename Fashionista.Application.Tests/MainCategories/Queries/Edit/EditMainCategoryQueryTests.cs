namespace Fashionista.Application.Tests.MainCategories.Queries.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.MainCategories.Commands.Edit;
    using Fashionista.Application.MainCategories.Queries.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditMainCategoryQueryTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "EditMainCategory query tests")]
        [Fact(DisplayName = "Handle given valid request should return EditMainCategoryCommand")]
        public async Task Handle_GivenValidRequest_ShouldReturnEditMainCategoryCommand()
        {
            // Arrange
            var query = new EditMainCategoryQuery { Id = 1 };
            var sut = new EditMainCategoryQueryHandler(this.deletableEntityRepository);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            command.ShouldNotBeNull();
            command.ShouldBeOfType<EditMainCategoryCommand>();
            command.Name.ShouldBe("Category1");
        }

        [Trait(nameof(MainCategory), "EditMainCategory query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new EditMainCategoryQuery { Id = 1000 };
            var sut = new EditMainCategoryQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(MainCategory), "EditMainCategory query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldTurnArgumentNullException()
        {
            // Arrange
            var sut = new EditMainCategoryQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}