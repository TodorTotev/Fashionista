namespace Fashionista.Application.Tests.Products.Queries.GetAllMenProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Products.Queries.GetAllMenProducts;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllMenProductsQueryTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "GetAllMenProducts query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllMenProductsQuery();
            var sut = new GetAllMenProductsQueryHandler(this.deletableEntityRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllMenProductsViewModel>();
            viewModel.Products.Count().ShouldBe(1);
            viewModel.Products.SingleOrDefault()?.ProductType.ShouldBe(ProductType.Men);
        }

        [Trait(nameof(Product), "GetAllMenProducts query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllMenProductsQueryHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}