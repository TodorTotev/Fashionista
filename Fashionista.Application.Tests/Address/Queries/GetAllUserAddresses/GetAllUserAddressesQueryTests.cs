namespace Fashionista.Application.Tests.Address.Queries.GetAllUserAddresses
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Addresses.Queries.GetAllUserAddresses;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetAllUserAddressesQueryTests : BaseTest<Address>
    {
        [Trait(nameof(Address), "GetAllUserAddresses query tests")]
        [Fact(DisplayName = "Handle given valid request should return view model")]
        public async Task Handle_GivenValidRequest_ShouldReturnViewModel()
        {
            // Arrange
            var user = await this.dbContext.Users.SingleOrDefaultAsync(x => x.FirstName == "TestFirstName");
            var command = new GetAllUserAddressesQuery { User = user };
            var sut = new GetAllUserAddressesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act
            var viewModel = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            viewModel.ShouldNotBeNull();
            viewModel.ShouldBeOfType<GetAllUserAddressesViewModel>();
            viewModel.Addresses.Count().ShouldBeGreaterThanOrEqualTo(1);
        }

        [Trait(nameof(Address), "GetAllUserAddresses query tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new GetAllUserAddressesQueryHandler(this.deletableEntityRepository, this.mapper);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}