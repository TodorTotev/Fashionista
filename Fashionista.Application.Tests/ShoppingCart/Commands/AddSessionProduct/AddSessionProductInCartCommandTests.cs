using System;
using Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts;

namespace Fashionista.Application.Tests.ShoppingCart.Commands.AddSessionProduct
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ShoppingCart.Commands.AddSesssion;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class AddSessionProductInCartCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "AddSessionProductInCart command tests")]
        [Fact(DisplayName = "Handle given valid request should create session")]
        public async Task Handle_GivenValidRequest_ShouldCreateSession()
        {
            // Arrange
            var shoppingCartAssistantMock = new Mock<IShoppingCartAssistant>();
            shoppingCartAssistantMock.Setup(x => x.Get()).Returns(new List<CartProductLookupModel>());

            var dummyList = new List<CartProductLookupModel>();

            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new AddSessionProductCartCommand { Id = 1, Session = dummyList };
            var sut = new AddSessionProductCartCommandHandler(productsRepository, shoppingCartAssistantMock.Object);

            // Act
            var session = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Arrange
            session.ShouldNotBeNull();
            session.Count.ShouldBe(1);
        }

        [Trait(nameof(ShoppingCartProduct), "AddSessionProductInCart command tests")]
        [Fact(DisplayName = "Handle given invalid ProductId should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var shoppingCartAssistantMock = new Mock<IShoppingCartAssistant>();
            shoppingCartAssistantMock.Setup(x => x.Get()).Returns(new List<CartProductLookupModel>());
            var dummyList = new List<CartProductLookupModel>();

            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new AddSessionProductCartCommand { Id = 100, Session = dummyList };
            var sut = new AddSessionProductCartCommandHandler(productsRepository, shoppingCartAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "AddSessionProductInCart command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddSessionProductCartCommandHandler(
                It.IsAny<EfDeletableEntityRepository<Product>>(),
                It.IsAny<IShoppingCartAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}