namespace Fashionista.Application.Tests.SubCategories.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.SubCategories.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteSubCategoryCommandTests : BaseTest<SubCategory>
    {
        [Trait(nameof(SubCategory), "DeleteSubCategory command tests")]
        [Fact(DisplayName = "Handle given valid request should delete category")]
        public async Task Handle_GivenValidRequest_ShouldDeleteCategory()
        {
            // Arrange
            var command = new DeleteSubCategoryCommand { Id = 1 };
            var sut = new DeleteSubCategoryCommandHandler(this.deletableEntityRepository);

            // Act
            var categoryId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var modifiedCategory = await this.dbContext.SubCategories
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            categoryId.ShouldBe(1);
            modifiedCategory.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(SubCategory), "DeleteSubCategory command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteSubCategoryCommandHandler(It.IsAny<IDeletableEntityRepository<SubCategory>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(SubCategory), "DeleteSubCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteSubCategoryCommand() { Id = 133 };
            var sut = new DeleteSubCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}