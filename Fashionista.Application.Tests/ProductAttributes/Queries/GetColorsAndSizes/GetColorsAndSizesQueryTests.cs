// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.ProductAttributes.Queries.GetColorsAndSizes
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductAttributes.Queries.GetColorsAndSizes;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetColorsAndSizesQueryTests : BaseTest<ProductAttributes>
    {
        [Trait(nameof(ProductAttributes), "GetColorsAndSizes query test")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetColorsAndSizesQuery { Id = 1 };
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var sut = new GetColorsAndSizesQueryHandler(
                this.deletableEntityRepository,
                productsRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<ProductColorsAndSizesViewModel>();
            viewModel.Colors.FirstOrDefault().ShouldNotBeNull();
            viewModel.Sizes.FirstOrDefault().ShouldNotBeNull();
            viewModel.Colors.FirstOrDefault().Name.ShouldBe("TestColor");
            viewModel.Sizes.FirstOrDefault().Name.ShouldBe("TestSize");
        }

        [Trait(nameof(ProductAttributes), "GetColorsAndSizes query test")]
        [Fact(DisplayName = "Handle given valid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetColorsAndSizesQuery { Id = 1000 };
            var productsRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var sut = new GetColorsAndSizesQueryHandler(
                this.deletableEntityRepository,
                productsRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "GetColorsAndSizes query test")]
        [Fact(DisplayName = "Handle given valid request should throw ArgumentNullException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetColorsAndSizesQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<EfDeletableEntityRepository<Product>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}