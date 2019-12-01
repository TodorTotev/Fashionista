// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Wishlist.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Application.Wishlist.Commands.Delete;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteFavoriteProductCommandTests : BaseTest<FavoriteProduct>
    {
        [Trait(nameof(FavoriteProduct), "DeleteFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given valid request should delete product")]
        public async Task Handle_GivenValidRequest_ShouldDeleteProduct()
        {
            // Arrange
            var userId = this.dbContext.Users.FirstOrDefault().Id;
            var userAccessorMock = new Mock<IUserAssistant>();
            userAccessorMock.Setup(x => x.UserId).Returns(userId);

            var command = new DeleteFavoriteProductCommand { Id = 1 };
            var sut = new DeleteFavoriteProductCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            this.dbContext.FavoriteProducts.Count().ShouldBe(0);
        }

        [Trait(nameof(FavoriteProduct), "DeleteFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given valid invalid request should throw NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = this.dbContext.Users.FirstOrDefault().Id;
            var userAccessorMock = new Mock<IUserAssistant>();
            userAccessorMock.Setup(x => x.UserId).Returns(userId);

            var command = new DeleteFavoriteProductCommand { Id = 100 };
            var sut = new DeleteFavoriteProductCommandHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(FavoriteProduct), "DeleteFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given valid null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteFavoriteProductCommandHandler(
                this.deletableEntityRepository,
                It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}