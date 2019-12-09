namespace Fashionista.Application.Tests.Address.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Addresses.Commands.Delete;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DeleteAddressCommandTests : BaseTest<Address>
    {
        [Trait(nameof(Address), "DeleteAddress command tests")]
        [Fact(DisplayName = "Handle given valid request should delete address")]
        public async Task Handle_GivenValidRequest_ShouldDeleteAddress()
        {
            // Arrange
            var command = new DeleteAddressCommand { Id = 1 };
            var sut = new DeleteAddressCommandHandler(this.deletableEntityRepository);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            id.ShouldBe(1);

            var deletedAddress = await this.dbContext.Addresses
                .FirstAsync(x => x.Id == id);

            deletedAddress.IsDeleted.ShouldBe(true);
        }

        [Trait(nameof(Address), "DeleteAddress command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw FailedDeletionException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowFailedDeletionException()
        {
            // Arrange
            var command = new DeleteAddressCommand { Id = 1 };
            var sut = new DeleteAddressCommandHandler(this.deletableEntityRepository);

            var address = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(1);

            this.deletableEntityRepository.Delete(address);
            await this.deletableEntityRepository.SaveChangesAsync();

            // Act & Assert
            await Should.ThrowAsync<FailedDeletionException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Address), "DeleteAddress command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new DeleteAddressCommand { Id = 1000 };
            var sut = new DeleteAddressCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Address), "DeleteAddress command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new DeleteAddressCommandHandler(this.deletableEntityRepository);

            // Act & assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}