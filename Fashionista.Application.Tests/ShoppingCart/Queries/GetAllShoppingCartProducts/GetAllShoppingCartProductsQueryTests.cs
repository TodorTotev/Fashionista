// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.ShoppingCart.Queries.GetAllShoppingCartProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ShoppingCart.Queries.GetAllShoppingCartProducts;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllShoppingCartProductsQueryTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "GetAllShoppingCartProducts query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var shoppingCartId = this.dbContext.Users.FirstOrDefault().ShoppingCartId;
            var userAccessorMock = new Mock<IUserAssistant>();
            userAccessorMock.Setup(x => x.ShoppingCartId).Returns(shoppingCartId);

            var command = new GetAllShoppingCartProductsQuery();
            var sut = new GetAllShoppingCartProductsQueryHandler(
                this.deletableEntityRepository,
                this.mapper,
                userAccessorMock.Object);

            // Act
            var viewModel = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<AllShoppingCartProductsViewModel>();
            viewModel.ShoppingCartProducts.Count().ShouldNotBeNull();
            viewModel.ShoppingCartProducts.Count().ShouldBe(1);

            viewModel.ShoppingCartProducts.FirstOrDefault().Quantity.ShouldBe(1);
            viewModel.ShoppingCartProducts.FirstOrDefault().ProductPhotos.ShouldNotBeNull();
            viewModel.ShoppingCartProducts.FirstOrDefault().ProductPhotos.Count().ShouldBe(1);
            viewModel.ShoppingCartProducts.FirstOrDefault().TotalPrice.ShouldBe(100);
        }

        [Trait(nameof(ShoppingCartProduct), "GetAllShoppingCartProducts query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllShoppingCartProductsQueryHandler(
                this.deletableEntityRepository,
                this.mapper,
                It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}