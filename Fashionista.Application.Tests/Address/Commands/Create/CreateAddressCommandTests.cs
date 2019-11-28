using System;
using Microsoft.EntityFrameworkCore;

namespace Fashionista.Application.Tests.Address.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Addresses.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
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
            var user = new ApplicationUser
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@mail.com",
                ShoppingCart = new ShoppingCart(),
            };
            var command = new CreateAddressCommand
            {
                Street = "NewStreet",
                Description = "NewDesc",
                City = "TestCity",
                Zip = "1000",
                User = user,
            };

            var sut = new CreateAddressCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBe(2);

            var address = await this.dbContext.Addresses
                .SingleOrDefaultAsync(x => x.Id == id);

            address.Name.ShouldBe("NewStreet");
            address.Description.ShouldBe("NewDesc");
            address.City.Name.ShouldBe("TestCity");
            address.ApplicationUser.Email.ShouldBe("Test@mail.com");
        }

        [Trait(nameof(Address), "CreateAddress command tests")]
        [Fact(DisplayName = "Handle given valid null request throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new CreateAddressCommandHandler(this.deletableEntityRepository, this.mediatorMock.Object);

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}