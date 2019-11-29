using AutoMapper;

namespace Fashionista.Application.Tests.SubCategories.Commands.Edit
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.SubCategories.Commands.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditSubCategoryCommandTests : BaseTest<SubCategory>
    {
        [Trait(nameof(SubCategory), "EditSubCategory command tests")]
        [Fact(DisplayName = "Handle given valid request should edit sub category")]
        public async Task Handle_GivenValidRequest_ShouldEditSubCategory()
        {
            // Arrange
            var command = new EditSubCategoryCommand
            {
                Id = 1,
                Name = "ModifiedCategory",
                Description = "ModifiedDesc",
            };

            var sut = new EditSubCategoryCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var modifiedCategory = await this.dbContext.SubCategories
                .FirstOrDefaultAsync(x => x.Id == id);

            modifiedCategory.ShouldNotBeNull();
            modifiedCategory.Name.ShouldBe("ModifiedCategory");
            modifiedCategory.Description.ShouldBe("ModifiedDesc");
        }

        [Trait(nameof(SubCategory), "EditSubCategory command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditSubCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(SubCategory), "EditSubCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new EditSubCategoryCommand
            {
                Name = "Category1",
                Description = "TestDesc",
            };

            var sut = new EditSubCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(SubCategory), "EditSubCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new EditSubCategoryCommand
            {
                Id = 1000,
                Name = "Category111",
                Description = "TestDesc111",
            };
            var sut = new EditSubCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}