// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.ShoppingCart.Commands.Add
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ShoppingCart.Commands.Add;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class AddProductInCartCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "AddProductInCart command tests")]
        [Fact(DisplayName = "Handle given valid request should add product in cart")]
        public async Task Handle_GivenValidRequest_ShouldAddProductInCart()
        {
            // Arrange
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new AddProductInCartCommand { Id = 2, Quantity = 2, ColorId = 1, SizeId = 1 };
            var sut = new AddProductInCartCommandHandler(
                this.deletableEntityRepository,
                productsRepository,
                this.userAssistantMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = this.dbContext.ShoppingCartProducts.FirstOrDefault(x => x.ProductId == id);
            product.ShoppingCartId.ShouldBe(this.userAssistantMock.Object.ShoppingCartId);
            product.Quantity.ShouldBe(2);
            product.ColorId.ShouldBe(1);
            product.SizeId.ShouldBe(1);
        }

        [Trait(nameof(ShoppingCartProduct), "AddProductInCart command tests")]
        [Fact(DisplayName = "Handle given invalid ProductId should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new AddProductInCartCommand { Id = 1000, Quantity = 2, ColorId = 1, SizeId = 1 };
            var sut = new AddProductInCartCommandHandler(
                this.deletableEntityRepository,
                productsRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "AddProductInCart command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw AlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new AddProductInCartCommand { Id = 1, Quantity = 2, ColorId = 1, SizeId = 1 };
            var sut = new AddProductInCartCommandHandler(
                this.deletableEntityRepository,
                productsRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "AddProductInCart command tests")]
        [Fact(DisplayName = "Handle given Null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddProductInCartCommandHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<Product>>(),
                It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}