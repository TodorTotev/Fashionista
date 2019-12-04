using System;
using System.Linq;
using Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts;
// ReSharper disable PossibleNullReferenceException

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
            var dummyList = new List<CartProductLookupModel>();

            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var sizesRepository = new EfDeletableEntityRepository<ProductSize>(this.dbContext);
            var colorsRepository = new EfDeletableEntityRepository<ProductColor>(this.dbContext);
            var command = new AddSessionProductInCartCommand
            {
                Id = 1,
                Session = dummyList,
                ColorId = 1,
                Quantity = 1,
                SizeId = 1,
            };
            var sut = new AddSessionProductInCartCommandHandler(
                productsRepository,
                this.shoppingCartAssistantMock.Object,
                sizesRepository,
                colorsRepository);

            // Act
            var session = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Arrange
            session.ShouldNotBeNull();
            session.Count.ShouldBe(1);
            session.FirstOrDefault().ColorName.ShouldBe("TestColor");
            session.FirstOrDefault().SizeName.ShouldBe("TestSize");
        }

        [Trait(nameof(ShoppingCartProduct), "AddSessionProductInCart command tests")]
        [Fact(DisplayName = "Handle given invalid ProductId should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var dummyList = new List<CartProductLookupModel>();

            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var sizesRepository = new EfDeletableEntityRepository<ProductSize>(this.dbContext);
            var colorsRepository = new EfDeletableEntityRepository<ProductColor>(this.dbContext);
            var command = new AddSessionProductInCartCommand
            {
                Id = 1000,
                Session = dummyList,
                ColorId = 1,
                Quantity = 1,
                SizeId = 1,
            };

            var sut = new AddSessionProductInCartCommandHandler(
                productsRepository,
                this.shoppingCartAssistantMock.Object,
                sizesRepository,
                colorsRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "AddSessionProductInCart command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddSessionProductInCartCommandHandler(
                It.IsAny<EfDeletableEntityRepository<Product>>(),
                It.IsAny<IShoppingCartAssistant>(),
                It.IsAny<EfDeletableEntityRepository<ProductSize>>(),
                It.IsAny<EfDeletableEntityRepository<ProductColor>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}