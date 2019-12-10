namespace Fashionista.Application.Tests.Products.Queries.GetAllProductsPaged
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Products.Queries.GetAllProductsPaged;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllProductsPagedQueryTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "GetAllProductsPaged query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllProductsPagedQuery { PageNumber = 0, PageSize = 3, IsActive = true };
            var sut = new GetAllProductsPagedQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllProductsPagedViewModel>();
            viewModel.Products.Count().ShouldBe(1);
        }

        [Trait(nameof(Product), "GetAllProductsPaged query tests")]
        [Fact(DisplayName = "Handle given valid request with IsActive = true should return only active products")]
        public async Task Handle_GivenValidRequest_ShouldReturnOnlyActiveProducts()
        {
            // Arrange
            var query = new GetAllProductsPagedQuery { PageNumber = 0, PageSize = 3, IsActive = true };
            var sut = new GetAllProductsPagedQueryHandler(this.deletableEntityRepository);

            // Act
            var listOfProducts = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            listOfProducts.Products.FirstOrDefault()?.Name.ShouldBe("ActiveProduct");
            listOfProducts.Products.FirstOrDefault()?.IsHidden.ShouldBe(false);
            listOfProducts.Products.Count().ShouldBe(1);
        }

        [Trait(nameof(Product), "GetAllProductsPaged query tests")]
        [Fact(DisplayName = "Handle given valid request with IsActive = false should return only draft products")]
        public async Task Handle_GivenValidRequest_ShouldReturnOnlyDraftProducts()
        {
            // Arrange
            var query = new GetAllProductsPagedQuery { PageNumber = 0, PageSize = 3, IsActive = false };
            var sut = new GetAllProductsPagedQueryHandler(this.deletableEntityRepository);

            // Act
            var listOfProducts = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            listOfProducts.Products.FirstOrDefault()?.Name.ShouldBe("DraftProduct");
            listOfProducts.Products.FirstOrDefault()?.IsHidden.ShouldBe(true);
            listOfProducts.Products.Count().ShouldBe(1);
        }

        [Trait(nameof(Product), "GetAllProductsPaged query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllProductsPagedQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}