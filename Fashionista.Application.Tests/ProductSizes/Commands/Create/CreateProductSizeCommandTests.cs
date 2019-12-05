namespace Fashionista.Application.Tests.ProductSizes.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductSizes.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateProductSizeCommandTests : BaseTest<ProductSize>
    {
        [Trait(nameof(ProductSize), "CreateProductSize command tests")]
        [Fact(DisplayName = "Handle given valid request should create size")]
        public async Task Handle_GivenValidRequest_ShouldCreateSize()
        {
            // Arrange
            var command = new CreateProductSizeCommand { Name = "NewSize", MainCategoryId = 1};
            var sut = new CreateProductSizeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(2);
            var createdSize = await this.dbContext.ProductSizes
                .FirstOrDefaultAsync(x => x.Id == id);

            createdSize.Name.ShouldBe("NewSize");
            createdSize.MainCategoryId.ShouldBe(1);
        }

        [Trait(nameof(ProductSize), "CreateProductSize command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateProductSizeCommand { Name = "TestSize", MainCategoryId = 1};
            var sut = new CreateProductSizeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductSize), "CreateProductSize command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateProductSizeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}