// ReSharper disable PossibleNullReferenceException

namespace Fashionista.Application.Tests.Brands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Fashionista.Application.Brands.Commands.Create;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateBrandCommandTests : BaseTest<Brand>
    {
        [Trait(nameof(Brand), "CreateBrand command tests")]
        [Fact(DisplayName = "Handle given valid request should create entity")]
        public async Task Handle_GivenValidRequest_ShouldCreateEntity()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            var cloudinaryMock = new Mock<Cloudinary>();
            var photoUrl = "https://imgur.com/uyT0KJ8";

            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()))
                .ReturnsAsync(photoUrl);

            var command = new CreateBrandCommand
            {
                Name = "ValidBrand",
                Photo = It.IsAny<IFormFile>(),
            };

            var sut = new CreateBrandCommandHandler(this.deletableEntityRepository, cloudinaryHelperMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            id.ShouldBeGreaterThan(0);

            var createdBrand = this.deletableEntityRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            createdBrand.Name = "ValidBrand";
            createdBrand.BrandPhotoUrl = photoUrl;
        }

        [Trait(nameof(Brand), "CreateBrand command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentException()
        {
            // Assert
            var sut = new CreateBrandCommandHandler(
                It.IsAny<IDeletableEntityRepository<Brand>>(), It.IsAny<ICloudinaryHelper>());

            // Act & Arrange
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Brand), "CreateBrand command tests")]
        [Fact(DisplayName = "Handle given invalid request should throw EntityAlreadyExistsException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var command = new CreateBrandCommand
            {
                Name = "TestBrand",
                Photo = It.IsAny<IFormFile>(),
            };

            var sut = new CreateBrandCommandHandler(this.deletableEntityRepository, It.IsAny<ICloudinaryHelper>());

            // Act & Assert
            await Should.ThrowAsync<EntityAlreadyExistsException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}