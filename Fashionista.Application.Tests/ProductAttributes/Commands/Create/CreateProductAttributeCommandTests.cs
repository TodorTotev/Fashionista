namespace Fashionista.Application.Tests.ProductAttributes.Commands.Create
{
    using System;
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

    public class CreateProductAttributeCommandTests : BaseTest<ProductAttributes>
    {
        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given valid request should create attribute")]
        public async Task Handle_GivenValidRequest_ShouldCreateAttribute()
        {
            // Arrange
            var command = new CreateProductAttributeCommand { Quantity = 1, ProductColorId = 1, ProductSizeId = 1, ProductId = 2 };
            var sut = new CreateProductAttributeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var action = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var attribute = await this.dbContext.ProductAttributes
                .FirstOrDefaultAsync(x => x.Id == action.Id);

            attribute.Id.ShouldBe(2);
            attribute.ShouldNotBeNull();
            attribute.ProductColor.Name.ShouldBe("TestColor");
            attribute.ProductSize.Name.ShouldBe("TestSize");
            attribute.Product.Name.ShouldBe("DraftProduct");
        }

        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateProductAttributeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateProductAttributeCommand { Quantity = 1, ProductColorId = 1, ProductSizeId = 1, ProductId = 1 };
            var sut = new CreateProductAttributeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}