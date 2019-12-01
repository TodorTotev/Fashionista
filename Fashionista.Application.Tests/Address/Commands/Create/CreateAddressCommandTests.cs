using System.Linq;
using Fashionista.Application.Interfaces;

// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Address.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Addresses.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateAddressCommandTests : BaseTest<Address>
    {
        [Trait(nameof(Address), "CreateAddress command tests")]
        [Fact(DisplayName = "Handle given valid request should create address")]
        public async Task Handle_GivenValidRequest_ShouldCreateAddress()
        {
            // Arrange
            var userId = this.dbContext.Users.FirstOrDefault().Id;
            var userAccessorMock = new Mock<IUserAssistant>();
            userAccessorMock.Setup(x => x.UserId).Returns(userId);

            var command = new CreateAddressCommand
            {
                Street = "NewStreet",
                Description = "NewDesc",
                City = "TestCity",
                Zip = "1000",
            };

            var sut = new CreateAddressCommandHandler(
            this.deletableEntityRepository,
            this.mediatorMock.Object,
            userAccessorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(2);

            var address = await this.dbContext.Addresses
                .SingleOrDefaultAsync(x => x.Id == id);

            address.Name.ShouldBe("NewStreet");
            address.Description.ShouldBe("NewDesc");
            address.City.Name.ShouldBe("TestCity");
            address.ApplicationUser.Email.ShouldBe("Test@test.com");
        }

        [Trait(nameof(Address), "CreateAddress command tests")]
        [Fact(DisplayName = "Handle given valid null request throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateAddressCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object, It.IsAny<IUserAssistant>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}