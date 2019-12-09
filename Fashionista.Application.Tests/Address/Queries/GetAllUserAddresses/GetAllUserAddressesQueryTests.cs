// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Address.Queries.GetAllUserAddresses
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Addresses.Queries.GetAllUserAddresses;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
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
            var userId = this.dbContext.Users.FirstOrDefault().Id;
            var userAccessorMock = new Mock<IUserAssistant>();
            userAccessorMock.Setup(x => x.UserId).Returns(userId);

            var command = new GetAllUserAddressesQuery();
            var sut = new GetAllUserAddressesQueryHandler(this.deletableEntityRepository, userAccessorMock.Object);

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
            var userAccessorMock = new Mock<IUserAssistant>();
            userAccessorMock.Setup(x => x.UserId).Returns("1");

            var sut = new GetAllUserAddressesQueryHandler(this.deletableEntityRepository, userAccessorMock.Object);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}