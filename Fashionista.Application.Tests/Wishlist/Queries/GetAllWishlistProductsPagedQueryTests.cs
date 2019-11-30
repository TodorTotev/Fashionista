namespace Fashionista.Application.Tests.Wishlist.Queries
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Application.Wishlist.Queries.GetAllWishlistProducts;
    using Fashionista.Application.Wishlist.Queries.GetAllWishlistProductsPaged;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllWishlistProductsPagedQueryTests : BaseTest<FavoriteProduct>
    {
        [Trait(nameof(Product), "GetAllWishlistProductsPaged query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var user = this.dbContext.Users.FirstOrDefault();
            var query = new GetAllWishlistProductsPagedQuery { PageNumber = 0, PageSize = 3, User = user };
            var sut = new GetAllWishlistProductsPagedQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<WishlistProductsViewModel>();
            viewModel.Products.Count().ShouldBe(1);
        }

        [Trait(nameof(Product), "GetAllWishlistProductsPaged query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllWishlistProductsPagedQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}