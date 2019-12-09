using System;
using Fashionista.Application.Exceptions;
using Fashionista.Application.Interfaces;

namespace Fashionista.Application.Tests.Brands.Commands.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Brands.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteBrandCommandTests : BaseTest<Brand>
    {
        [Trait(nameof(Brand), "DeleteBrand command tests")]
        [Fact(DisplayName = "Handle given valid request should delete brand")]
        public async Task Handle_GivenValidRequest_ShouldDeleteBrand()
        {
            // Arrange
            var command = new DeleteBrandCommand { Id = 1 };
            var sut = new DeleteBrandCommandHandler(this.deletableEntityRepository);

            // Act
            var brandId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var deletedBrand = await this.dbContext.Brands
                .FirstOrDefaultAsync(x => x.Id == brandId);

            deletedBrand.IsDeleted.ShouldBe(true);
            brandId.ShouldBe(1);
        }

        [Trait(nameof(Brand), "DeleteBrand command tests")]
        [Fact(DisplayName = "Handle invalid valid request should throw FailedDeletionException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowFailedDeletionException()
        {
            // Arrange
            var command = new DeleteBrandCommand { Id = 1 };
            var sut = new DeleteBrandCommandHandler(this.deletableEntityRepository);

            var brand = await this.deletableEntityRepository.GetByIdWithDeletedAsync(1);

            this.deletableEntityRepository.Delete(brand);
            await this.deletableEntityRepository.SaveChangesAsync();

            // Act & Assert
            await Should.ThrowAsync<FailedDeletionException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Brand), "DeleteBrand command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteBrandCommandHandler(It.IsAny<IDeletableEntityRepository<Brand>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Brand), "DeleteBrand command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityNotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var command = new DeleteBrandCommand { Id = 1000 };
            var sut = new DeleteBrandCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}