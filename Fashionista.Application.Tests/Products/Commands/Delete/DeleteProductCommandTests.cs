namespace Fashionista.Application.Tests.Products.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Products.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteProductCommandTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "DeleteProduct command tests")]
        [Fact(DisplayName = "Handle given valid request should delete product")]
        public async Task Handle_GivenValidRequest_ShouldDeleteCategory()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = 1 };
            var sut = new DeleteProductCommandHandler(this.deletableEntityRepository);

            // Act
            var productId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            productId.ShouldBe(1);

            var modifiedProduct = await this.dbContext.Products
                .FindAsync(productId);
            modifiedProduct.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(Product), "DeleteProduct command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteProductCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "DeleteProduct command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteProductCommand { Id = 100 };
            var sut = new DeleteProductCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}