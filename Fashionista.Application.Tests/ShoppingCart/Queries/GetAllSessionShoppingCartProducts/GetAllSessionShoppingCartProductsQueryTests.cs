// ReSharper disable PossibleNullReferenceException
// ReSharper disable HeapView.ClosureAllocation

using System;

namespace Fashionista.Application.Tests.ShoppingCart.Queries.GetAllSessionShoppingCartProducts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ShoppingCart.Queries.GetAllSessionShoppingCartProducts;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllSessionShoppingCartProductsQueryTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(Product), "GetAllSessionShoppingCartProducts query tests")]
        [Fact(DisplayName = "Handle given valid request should return session products")]
        public async Task Handle_GivenValidRequest_ShouldReturnSessionProducts()
        {
            // Arrange
            var command = new GetAllSessionShoppingCartProductsQuery();
            var sut = new GetAllSessionShoppingCartProductsQueryHandler(this.shoppingCartAssistantMock.Object);

            // Act
            var session = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            session.ShouldNotBeNull();
            session.ShouldBeOfType<List<CartProductLookupModel>>();
            session.Count().ShouldBe(2);
        }

        [Trait(nameof(Product), "GetAllSessionShoppingCartProducts query tests")]
        [Fact(DisplayName = "Handle given valid request should return session products")]
        public async Task Handle_GivenNullRequestShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllSessionShoppingCartProductsQueryHandler(It.IsAny<IShoppingCartAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}