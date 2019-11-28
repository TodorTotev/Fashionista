using System;
using Fashionista.Application.Exceptions;
using Fashionista.Application.ProductAttributes.Commands.Edit;

namespace Fashionista.Application.Tests.Cities.Queries.GetCity
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Cities.Queries.GetCity;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetCityQueryTests : BaseTest<City>
    {
        [Trait(nameof(City), "GetCity query tests")]
        [Fact(DisplayName = "Handle given valid request should return city")]
        public async Task Handle_GivenValidRequest_ShouldReturnCity()
        {
            // Arrange
            var query = new GetCityQuery { Name = "TestCity" };
            var sut = new GetCityQueryHandler(this.deletableEntityRepository);

            // Act
            var city = await sut.Handle(query, It.IsAny<CancellationToken>());

            city.Name.ShouldBe("TestCity");
            city.Postcode.ShouldBe("TestPostCode");
        }

        [Trait(nameof(City), "GetCity query tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new GetCityQuery { Name = "Invalid" };
            var sut = new GetCityQueryHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(City), "GetCity query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetCityQueryHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        
    }
}