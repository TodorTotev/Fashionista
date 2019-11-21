using System;
using Fashionista.Application.Exceptions;

namespace Fashionista.Application.Tests.Products.Queries.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Products.Commands.Edit;
    using Fashionista.Application.Products.Queries.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class EditProductQueryTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "EditProduct query tests")]
        [Fact(DisplayName = "Handle given valid request should return command")]
        public async Task Handle_GivenValidRequest_ShouldReturnCommand()
        {
            // Arrange
            var query = new EditProductQuery { Id = 1 };
            var sut = new EditProductQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var command = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            command.ShouldNotBeNull();
            command.ShouldBeOfType<EditProductCommand>();
            command.Name.ShouldBe("ActiveProduct");
        }

        [Trait(nameof(Product), "EditProduct query tests")]
        [Fact(DisplayName = "Handle given invalid request should return NotFoundException ")]
        public async Task Handle_GivenInvalidRequest_ShouldReturnNotFoundException()
        {
            // Arrange
            var query = new EditProductQuery { Id = 1000 };
            var sut = new EditProductQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "EditProduct query tests")]
        [Fact(DisplayName = "Handle given null request should return ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldReturnArgumentNullException()
        {
            // Arrange
            var sut = new EditProductQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}