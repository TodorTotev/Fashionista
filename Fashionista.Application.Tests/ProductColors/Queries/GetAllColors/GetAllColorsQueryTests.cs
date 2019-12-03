namespace Fashionista.Application.Tests.ProductColors.Queries.GetAllColors
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.ProductColors.Queries.GetAllColors;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllColorsQueryTests : BaseTest<ProductColor>
    {
        [Trait(nameof(ProductColor), "GetAllColors query tests")]
        [Fact(DisplayName = "Handle given valid request should return colors")]
        public async Task Handle_GivenValidRequest_ShouldReturnColors()
        {
            // Arrange
            var query = new GetAllColorsQuery();
            var sut = new GetAllColorsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var list = await sut.Handle(query, It.IsAny<CancellationToken>());

            // Assert
            list.ShouldNotBeNull();
            list.ShouldBeOfType<List<ProductColorLookupModel>>();
            list.Count.ShouldBe(1);
        }

        [Trait(nameof(ProductColor), "GetAllColors query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllColorsQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}