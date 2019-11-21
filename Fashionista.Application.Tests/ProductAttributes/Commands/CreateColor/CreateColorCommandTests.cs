using System;

namespace Fashionista.Application.Tests.ProductAttributes.Commands.CreateColor
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ProductAttributes.Commands.CreateColor;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateColorCommandTests : BaseTest<ProductColor>
    {
        [Trait(nameof(ProductColor), "CreateColor command tests")]
        [Fact(DisplayName = "Handle given valid request should create color")]
        public async Task Handle_GivenValidRequest_ShouldCreateColor()
        {
            // Arrange
            var command = new CreateColorCommand { Name = "TestColor" };
            var sut = new CreateColorCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var createdColor = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(id);

            createdColor.Id.ShouldBe(1);
            createdColor.Name.ShouldBe("TestColor");
        }

        [Trait(nameof(ProductColor), "CreateColor command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateColorCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}