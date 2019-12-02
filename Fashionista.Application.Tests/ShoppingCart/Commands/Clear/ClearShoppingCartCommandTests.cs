namespace Fashionista.Application.Tests.ShoppingCart.Commands.Clear
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ShoppingCart.Commands.Clear;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ClearShoppingCartCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "ClearShoppingCart command tests")]
        [Fact(DisplayName = "Handle given valid request should clear user cart products")]
        public async Task Handle_GivenValidRequest_ShouldClearUserCartProducts()
        {
            // Arrange
            var command = new ClearShoppingCartCommand();
            var sut = new ClearShoppingCartCommandHandler(this.deletableEntityRepository, this.userAssistantMock.Object);

            // Act
            var count = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            count.ShouldBe(1);
        }

        [Trait(nameof(ShoppingCartProduct), "ClearShoppingCart command tests")]
        [Fact(DisplayName = "Handle given valid null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ClearShoppingCartCommandHandler(this.deletableEntityRepository, this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}