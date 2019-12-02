namespace Fashionista.Application.Tests.ShoppingCart.Commands.ClearSession
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ShoppingCart.Commands.ClearSession;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ClearSessionShoppingCartCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "ClearSessionShoppingCart command tests")]
        [Fact(DisplayName = "Handle given valid request should clear user cart products")]
        public async Task Handle_GivenValidRequest_ShouldClearUserCartProducts()
        {
            // Arrange
            var products = this.shoppingCartAssistantMock.Object.Get();
            var command = new ClearSessionShoppingCartCommand { Session = products };
            var sut = new ClearSessionShoppingCartCommandHandler(this.shoppingCartAssistantMock.Object);

            // Act
            var session = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            session.Count.ShouldBe(0);
        }

        [Trait(nameof(ShoppingCartProduct), "ClearSessionShoppingCart command tests")]
        [Fact(DisplayName = "Handle given valid null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new ClearSessionShoppingCartCommandHandler(this.shoppingCartAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}