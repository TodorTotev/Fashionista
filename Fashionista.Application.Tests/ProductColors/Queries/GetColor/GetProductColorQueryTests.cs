namespace Fashionista.Application.Tests.ProductColors.Queries.GetColor
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.ProductColors.Queries.GetColor;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetProductColorQueryTests : BaseTest<ProductColor>
    {
        [Trait(nameof(ProductColor), "GetProductColor query tests")]
        [Fact(DisplayName = "Handle given valid request should return color")]
        public async Task Handle_GivenValidRequest_ShouldReturnColor()
        {
            // Arrange
            var query = new GetProductColorQuery { Id = 1 };
            var sut = new GetProductColorQueryHandler(this.deletableEntityRepository);

            // Act
            var color = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            color.ShouldNotBeNull();
            color.ShouldBeOfType<ProductColorLookupModel>();
            color.Name.ShouldBe("TestColor");
        }

        [Trait(nameof(ProductColor), "GetColor query tests")]
        [Fact(DisplayName = "Handle given invalid request should return NotFoundException")]
        public async Task Handle_GivenValidRequest_ShouldReturnNotFoundException()
        {
            // Arrange
            var query = new GetProductColorQuery { Id = 1000 };
            var sut = new GetProductColorQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(ProductColor), "GetColor query tests")]
        [Fact(DisplayName = "Handle given null request should return ArgumentNullException")]
        public async Task Handle_GivenValidRequest_ShouldReturnArgumentNullException()
        {
            // Arrange
            var sut = new GetProductColorQueryHandler(this.deletableEntityRepository);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}