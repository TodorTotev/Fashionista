namespace Fashionista.Application.Tests.MainCategories.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.MainCategories.Commands.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditMainCategoryCommandTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "EditMainCategory command tests")]
        [Fact(DisplayName = "Handle given valid request should edit main category")]
        public async Task Handle_GivenValidRequest_ShouldEditMainCategory()
        {
            // Arrange
            var command = new EditMainCategoryCommand
            {
                Id = 1,
                Name = "ModifiedCategory",
            };

            var sut = new EditMainCategoryCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var modifiedCategory = await this.dbContext.MainCategories
                .FirstOrDefaultAsync(x => x.Id == id);

            modifiedCategory.ShouldNotBeNull();
            modifiedCategory.Name.ShouldBe("ModifiedCategory");
        }

        [Trait(nameof(MainCategory), "EditMainCategory command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditMainCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(MainCategory), "EditMainCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new EditMainCategoryCommand()
            {
                Name = "Category1",
            };

            var sut = new EditMainCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}