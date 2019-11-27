using System;
using Fashionista.Application.Exceptions;
using Moq;

namespace Fashionista.Application.Tests.ProductAttributes.Commands.Delete
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ProductAttributes.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
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
                .GetByIdWithDeletedAsync(id);

            deletedEntity.IsDeleted.ShouldBe(true);
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