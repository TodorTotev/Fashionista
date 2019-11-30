// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Wishlist.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Application.Wishlist.Commands.Create;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateFavoriteProductCommandTests : BaseTest<FavoriteProduct>
    {
        [Trait(nameof(FavoriteProduct), "CreateFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given valid request should create favorite product")]
        public async Task Handle_GivenValidRequest_ShouldCreateFavoriteProduct()
        {
            // Arrange
            var user = this.dbContext.Users.FirstOrDefault();
            var productRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new CreateFavoriteProductCommand {User = user, Id = 2};
            var sut = new CreateFavoriteProductCommandHandler(this.deletableEntityRepository, productRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(2);

            var favoriteProduct = this.dbContext.FavoriteProducts.FirstOrDefault(x => x.ProductId == id);
            favoriteProduct.ApplicationUserId.ShouldBe(user.Id);
        }

        [Trait(nameof(FavoriteProduct), "CreateFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given invalid product id should throw NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var user = this.dbContext.Users.FirstOrDefault();
            var productRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new CreateFavoriteProductCommand {User = user, Id = 500};
            var sut = new CreateFavoriteProductCommandHandler(this.deletableEntityRepository, productRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(FavoriteProduct), "CreateFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given invalid request should EntityAlreadyExistsException")]
        public async Task Handle_GivenValidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var user = this.dbContext.Users.FirstOrDefault();
            var productRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var command = new CreateFavoriteProductCommand {User = user, Id = 1};
            var sut = new CreateFavoriteProductCommandHandler(this.deletableEntityRepository, productRepository);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(FavoriteProduct), "CreateFavoriteProduct command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange;
            var sut = new CreateFavoriteProductCommandHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<Product>>());

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}