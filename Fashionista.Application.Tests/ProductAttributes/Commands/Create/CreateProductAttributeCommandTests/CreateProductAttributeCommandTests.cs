using System;

namespace Fashionista.Application.Tests.ProductAttributes.Commands.Create.CreateProductAttributeCommandTests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateProductAttributeCommandTests : BaseTest<Domain.Entities.ProductAttributes>
    {
        [Trait(nameof(ProductAttributes), "CreateProductAttributes command tests")]
        [Fact(DisplayName = "Handle given valid request should create attribute")]
        public async Task Handle_GivenValidRequest_ShouldCreateAttribute()
        {
            // Arrange
            var command = new CreateProductAttributeCommand { Quantity = 1, ProductColorId = 1, ProductSizeId = 1 };
            var sut = new CreateProductAttributeCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var attribute = await this.dbContext.ProductAttributes
                .FirstOrDefaultAsync(x => x.Id == id);

            id.ShouldBe(2);
            attribute.ShouldNotBeNull();
            attribute.ProductColor.Name.ShouldBe("TestColor");
            attribute.ProductSize.Name.ShouldBe("TestSize");
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
    }
}