namespace Fashionista.Application.Tests.ProductColors.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductColors.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateProductColorCommandTests : BaseTest<ProductColor>
    {
        [Trait(nameof(ProductColor), "CreateProductColor command tests")]
        [Fact(DisplayName = "Handle given valid request should create color")]
        public async Task Handle_GivenValidRequest_ShouldCreateColor()
        {
            // Arrange
            var command = new CreateProductColorCommand { Name = "NewColor" };
            var sut = new CreateProductColorCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var createdColor = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(id);

            createdColor.Id.ShouldBe(2);
            createdColor.Name.ShouldBe("NewColor");
        }

        [Trait(nameof(ProductColor), "CreateProductColor command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateProductColorCommand { Name = "TestColor" };
            var sut = new CreateProductColorCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductColor), "CreateColor command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateProductColorCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}