// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.ProductAttributes.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateProductAttributeCommandTests : BaseTest<Product>
    {
        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given valid request should create attribute")]
        public async Task Handle_GivenValidRequest_ShouldCreateAttribute()
        {
            // Arrange
            var command = new CreateProductAttributeCommand
                { Quantity = 1, ProductColorId = 1, ProductSizeId = 1, ProductId = 2 };
            var sut = new AddProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act
            var productId = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = await this.dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            product.Id.ShouldBe(2);
            product.ProductAttributes.ShouldNotBeNull();
            var attribute = product.ProductAttributes.FirstOrDefault();
            attribute.ProductColor.Name.ShouldBe("TestColor");
            attribute.ProductSize.Name.ShouldBe("TestSize");
            attribute.Product.Name.ShouldBe("DraftProduct");
        }

        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new AddProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateProductAttributeCommand
                { Quantity = 1, ProductColorId = 1, ProductSizeId = 1, ProductId = 1 };
            var sut = new AddProductAttributeCommandHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}