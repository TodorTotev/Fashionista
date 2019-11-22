namespace Fashionista.Application.Tests.ProductAttributes.Queries.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductAttributes.Commands.Create;
    using Fashionista.Application.ProductAttributes.Queries.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateProductAttributeQueryTests : BaseTest<Product>
    {
        [Trait(nameof(ProductAttributes), "CreateProductAttribute query tests")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new CreateProductAttributeQuery { Id = 1 };
            var sut = new CreateProductAttributeQueryHandler(this.deletableEntityRepository);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ProductId.ShouldBe(1);
            command.ProductName.ShouldBe("ActiveProduct");
            command.ShouldBeOfType<CreateProductAttributeCommand>();
        }

        [Trait(nameof(ProductAttributes), "CreateProductAttribute query tests")]
        [Fact(DisplayName = "Handle given invalid request should return NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldReturnNotFoundException()
        {
            // Arrange
            var query = new CreateProductAttributeQuery { Id = 100 };
            var sut = new CreateProductAttributeQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductAttributes), "CreateProductAttribute query tests")]
        [Fact(DisplayName = "Handle given null request should return ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateProductAttributeQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}