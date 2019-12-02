// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.ShoppingCart.Commands.Edit
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ShoppingCart.Commands.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditShoppingCartProductCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "EditShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given valid request should return id")]
        public async Task Handle_GivenValidRequest_ShouldReturnId()
        {
            // Arrange
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new EditShoppingCartProductCommand { Id = 1, Quantity = 5 };
            var sut = new EditShoppingCartProductCommandHandler(
                productsRepository,
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = this.dbContext.ShoppingCartProducts
                .FirstOrDefault(x =>
                    x.ProductId == id
                    && x.ShoppingCartId == this.userAssistantMock.Object.ShoppingCartId);

            product.Quantity.ShouldBe(5);
        }

        [Trait(nameof(ShoppingCartProduct), "EditShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given valid invalid should not update")]
        public async Task Handle_GivenInvalidRequest_ShouldNotUpdate()
        {
            // Arrange
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new EditShoppingCartProductCommand { Id = 1, Quantity = 1 };
            var sut = new EditShoppingCartProductCommandHandler(
                productsRepository,
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = this.dbContext.ShoppingCartProducts
                .FirstOrDefault(x =>
                    x.ProductId == id
                    && x.ShoppingCartId == this.userAssistantMock.Object.ShoppingCartId);

            product.Quantity.ShouldBe(1);
        }

        [Trait(nameof(ShoppingCartProduct), "EditShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given invalid ProductId should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new EditShoppingCartProductCommand { Id = 1000, Quantity = 5 };
            var sut = new EditShoppingCartProductCommandHandler(
                productsRepository,
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "EditShoppingCartProduct command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditShoppingCartProductCommandHandler(
                It.IsAny<EfDeletableEntityRepository<Product>>(),
                It.IsAny<IDeletableEntityRepository<ShoppingCartProduct>>(),
                It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}