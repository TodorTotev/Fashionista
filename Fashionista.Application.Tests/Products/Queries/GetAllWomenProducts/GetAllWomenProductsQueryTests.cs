namespace Fashionista.Application.Tests.Products.Queries.GetAllWomenProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Products.Queries.GetAllWomenProducts;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllWomenProductsQueryTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "GetAllWomenProducts query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllWomenProductsQuery();
            var sut = new GetAllWomenProductsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllWomenProductsViewModel>();
            viewModel.Products.Count().ShouldBe(1);
            viewModel.Products.SingleOrDefault()?.ProductType.ShouldBe(ProductType.Women);
        }

        [Trait(nameof(Product), "GetAllMenProducts query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllWomenProductsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}