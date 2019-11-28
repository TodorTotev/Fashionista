using System;

namespace Fashionista.Application.Tests.Cities.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Cities.Commands;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateCityCommandTests : BaseTest<City>
    {
        [Trait(nameof(City), "CreateCity command tests")]
        [Fact(DisplayName = "Handle given valid request should create city")]
        public async Task Handle_GivenValidRequest_ShouldCreateCity()
        {
            // Arrange
            var command = new CreateCityCommand { Name = "NewCity", Postcode = "NewZip" };
            var sut = new CreateCityCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            id.ShouldBe(2);

            var city = await this.dbContext.Cities
                .SingleOrDefaultAsync(x => x.Id == id);

            city.Name.ShouldBe("NewCity");
            city.Postcode.ShouldBe("NewZip");
        }

        [Trait(nameof(City), "CreateCity command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateCityCommand { Name = "TestCity", Postcode = "TestZip" };
            var sut = new CreateCityCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(
                sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(City), "CreateCity command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateCityCommandHandler(this.deletableEntityRepository, this.mapper);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(
                sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}