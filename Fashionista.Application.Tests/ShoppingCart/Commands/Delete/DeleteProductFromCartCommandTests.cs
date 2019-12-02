// ReSharper disable PossibleNullReferenceException
namespace Fashionista.Application.Tests.ShoppingCart.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ShoppingCart.Commands.Delete;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteProductFromCartCommandTests : BaseTest<ShoppingCartProduct>
    {
        [Trait(nameof(ShoppingCartProduct), "DeleteProductFromCart command tests")]
        [Fact(DisplayName = "Handle given valid request should return id")]
        public async Task Handle_GivenValidRequest_ShouldReturnId()
        {
            // Arrange
            var command = new DeleteProductFromCartCommand { Id = 1 };
            var sut = new DeleteProductFromCartCommandHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = this.dbContext.ShoppingCartProducts
                .FirstOrDefault(x =>
                    x.ProductId == id
                    && x.ShoppingCartId == this.userAssistantMock.Object.ShoppingCartId);

            product.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(ShoppingCartProduct), "DeleteProductFromCart command tests")]
        [Fact(DisplayName = "Handle given invalid ProductId should throw NotFoundException")]
        public async Task Handle_GivenInvalidProductId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteProductFromCartCommand { Id = 1000 };
            var sut = new DeleteProductFromCartCommandHandler(
                this.deletableEntityRepository,
                this.userAssistantMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ShoppingCartProduct), "DeleteProductFromCart command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteProductFromCartCommandHandler(
                It.IsAny<IDeletableEntityRepository<ShoppingCartProduct>>(),
                It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}