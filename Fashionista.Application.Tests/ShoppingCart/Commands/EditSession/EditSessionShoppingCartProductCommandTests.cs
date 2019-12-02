// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.ShoppingCart.Commands.EditSession
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ShoppingCart.Commands.EditSession;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditSessionShoppingCartProductCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "EditSessionShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given valid request should return modified session")]
        public async Task Handle_GivenValidRequest_ShouldReturnModifiedSession()
        {
            // Arrange
            var products = this.shoppingCartAssistantMock.Object.Get();
            var command = new EditSessionShoppingCartProductCommand { Id = 1, Quantity = 5, Session = products };
            var sut = new EditSessionShoppingCartProductCommandHandler(this.shoppingCartAssistantMock.Object);

            // Act
            var session = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = session.FirstOrDefault(x => x.ProductId == 1);

            session.ShouldNotBeNull();
            session.Count.ShouldBe(2);
            product.Quantity.ShouldBe(5);
        }

        [Trait(nameof(ShoppingCartProduct), "EditSessionShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given invalid request should return return null")]
        public async Task Handle_GivenInvalidRequest_ShouldReturnNull()
        {
            // Arrange
            var products = this.shoppingCartAssistantMock.Object.Get();
            var command = new EditSessionShoppingCartProductCommand { Id = 1, Quantity = 0, Session = products };
            var sut = new EditSessionShoppingCartProductCommandHandler(this.shoppingCartAssistantMock.Object);

            // Act
            var session = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            session.ShouldBeNull();
        }

        [Trait(nameof(ShoppingCartProduct), "EditSessionShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given invalid ProductId should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var products = this.shoppingCartAssistantMock.Object.Get();
            var command = new EditSessionShoppingCartProductCommand { Id = 100, Quantity = 5, Session = products };
            var sut = new EditSessionShoppingCartProductCommandHandler(this.shoppingCartAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "EditSessionShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditSessionShoppingCartProductCommandHandler(this.shoppingCartAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}