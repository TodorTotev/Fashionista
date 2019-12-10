namespace Fashionista.Application.Tests.ProductAttributes.Queries.GetAll
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.ProductAttributes.Queries.GetAll;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllProductAttributesQueryTests : BaseTest<ProductAttributes>
    {
        [Trait(nameof(ProductAttributes), "GetAllProductAttributes query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var query = new GetAllProductAttributesQuery { Id = 1 };
            var productRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var sut = new GetAllProductAttributesQueryHandler(this.deletableEntityRepository, productRepository);

            // Act
            var viewModel = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllProductAttributesViewModel>();
            viewModel.ProductAttributesList.Count().ShouldBe(1);
        }

        [Trait(nameof(ProductAttributes), "GetAllProductAttributes query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var query = new GetAllProductAttributesQuery { Id = 1000 };
            var productRepository = new EfDeletableEntityRepository<Product>(this.dbContext);
            var sut = new GetAllProductAttributesQueryHandler(this.deletableEntityRepository, productRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "GetAllProductAttributes query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllProductAttributesQueryHandler(
                this.deletableEntityRepository,
                It.IsAny<IDeletableEntityRepository<Product>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}