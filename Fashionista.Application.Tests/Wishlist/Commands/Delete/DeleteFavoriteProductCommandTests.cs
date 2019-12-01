// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Wishlist.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
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
            var user = this.dbContext.Users.FirstOrDefault();
            var command = new DeleteFavoriteProductCommand { User = user, Id = 1 };
            var sut = new DeleteFavoriteProductCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            this.dbContext.FavoriteProducts.Count().ShouldBe(0);
        }

        [Trait(nameof(FavoriteProduct), "DeleteFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given valid invalid request should throw NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var user = this.dbContext.Users.FirstOrDefault();
            var command = new DeleteFavoriteProductCommand { User = user, Id = 100 };
            var sut = new DeleteFavoriteProductCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(FavoriteProduct), "DeleteFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given valid null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteFavoriteProductCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}