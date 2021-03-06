// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.MainCategories.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.MainCategories.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteMainCategoryCommandTests : BaseTest<MainCategory>
    {
        [Trait(nameof(MainCategory), "DeleteMainCategory command tests")]
        [Fact(DisplayName = "Handle given valid request should delete category")]
        public async Task Handle_GivenValidRequest_ShouldDeleteCategory()
        {
            // Arrange
            var command = new DeleteMainCategoryCommand { Id = 1 };
            var sut = new DeleteMainCategoryCommandHandler(this.deletableEntityRepository);

            // Act
            var categoryId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            categoryId.ShouldBe(1);

            var modifiedCategory = await this.dbContext.MainCategories
                .SingleOrDefaultAsync(x => x.Id == categoryId);
            modifiedCategory.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(MainCategory), "DeleteMainCategory command tests")]
        [Fact(DisplayName = "Handle given valid request should delete category")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowFailedDeletionException()
        {
            // Arrange
            var command = new DeleteMainCategoryCommand { Id = 1 };
            var sut = new DeleteMainCategoryCommandHandler(this.deletableEntityRepository);

            var category = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(1);

            this.deletableEntityRepository.Delete(category);
            await this.deletableEntityRepository.SaveChangesAsync();

            // Act & Assert
            await Should.ThrowAsync<FailedDeletionException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(MainCategory), "DeleteMainCategory command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteMainCategoryCommandHandler(It.IsAny<IDeletableEntityRepository<MainCategory>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(MainCategory), "DeleteMainCategory command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteMainCategoryCommand { Id = 133 };
            var sut = new DeleteMainCategoryCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}