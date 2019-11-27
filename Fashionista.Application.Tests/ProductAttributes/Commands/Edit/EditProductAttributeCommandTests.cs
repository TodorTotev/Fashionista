using System;
using Fashionista.Application.Exceptions;

namespace Fashionista.Application.Tests.ProductAttributes.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ProductAttributes.Commands.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditProductAttributeCommandTests : BaseTest<ProductAttributes>
    {
        [Trait(nameof(ProductAttributes), "EditProductAttribute command tests")]
        [Fact(DisplayName = "Handle given valid request should edit attribute")]
        public async Task Handle_GivenValidRequest_ShouldEditAttribute()
        {
            // Arrange
            var command = new EditProductAttributeCommand { ProductId = 1, Quantity = 5 };
            var sut = new EditProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(1);
            var editedEntity = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(id);

            editedEntity.Quantity.ShouldBe(5);
        }

        [Trait(nameof(ProductAttributes), "DeleteProductAttribute command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new EditProductAttributeCommand { ProductId = 1000, Quantity = 100 };
            var sut = new EditProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "DeleteProductAttribute command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}