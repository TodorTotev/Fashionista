namespace Fashionista.Application.Tests.ProductAttributes.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductAttributes.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteProductAttributeCommandTests : BaseTest<ProductAttributes>
    {
        [Trait(nameof(ProductAttributes), "DeleteProductAttribute command tests")]
        [Fact(DisplayName = "Handle given valid requests should delete attribute")]
        public async Task Handle_GivenValidRequest_ShouldDeleteAttribute()
        {
            // Arrange
            var command = new DeleteProductAttributeCommand { Id = 1 };
            var sut = new DeleteProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(1);

            var deletedEntity = await this.deletableEntityRepository
                .AllWithDeleted()
                .SingleOrDefaultAsync(x => x.Id == 1);

            deletedEntity.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(ProductAttributes), "DeleteProductAttribute command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw FailedDeletionException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowFailedDeletionException()
        {
            // Arrange
            var command = new DeleteProductAttributeCommand { Id = 1 };
            var sut = new DeleteProductAttributeCommandHandler(this.deletableEntityRepository);

            var attribute = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(1);

            this.deletableEntityRepository.Delete(attribute);
            await this.deletableEntityRepository.SaveChangesAsync();

            // Act & Assert
            await Should.ThrowAsync<FailedDeletionException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "DeleteProductAttribute command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteProductAttributeCommand { Id = 1000 };
            var sut = new DeleteProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "DeleteProductAttribute command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}