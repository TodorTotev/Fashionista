using System;
using Fashionista.Application.Exceptions;
using Shouldly;

namespace Fashionista.Application.Tests.Products.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Products.Commands.Edit;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Persistence.Repositories;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class EditProductCommandTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "EditProduct command tests")]
        [Fact(DisplayName = "Handle given valid request should modify product")]
        public async Task Handle_GivenValidRequest_ShouldModifyProduct()
        {
            // Arrange
            var command = new EditProductCommand
            {
                Id = 1,
                Name = "ModifiedProduct",
                Description = "ModifiedDescription",
                IsHidden = false,
                Price = 1,
                SubCategoryId = 1,
                ProductType = ProductType.Women,
                BrandId = 1,
            };

            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()));

            var brandRepository = new EfDeletableEntityRepository<Brand>(this.dbContext);
            var sut = new EditProductCommandHandler(this.deletableEntityRepository, brandRepository, cloudinaryHelperMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var product = await this.deletableEntityRepository
                .GetByIdWithDeletedAsync(id);

            id.ShouldBe(1);
            product.Name.ShouldBe("ModifiedProduct");
            product.Description.ShouldBe("ModifiedDescription");
            product.IsHidden = false;
            product.Price = 1;
            product.SubCategoryId = 1;
            product.ProductType = ProductType.Women;
        }

        [Trait(nameof(Product), "EditProduct command test")]
        [Fact(DisplayName = "Handle given invalid request should throw NotFoundException")]
        public async Task Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()));

            var command = new EditProductCommand
            {
                Id = 1,
                Name = "ModifiedProduct",
                Description = "ModifiedDescription",
                IsHidden = false,
                Price = 1,
                SubCategoryId = 1,
                ProductType = ProductType.Women,
                BrandId = 50,
            };

            var brandsRepository = new EfDeletableEntityRepository<Brand>(this.dbContext);
            var sut = new EditProductCommandHandler(
                this.deletableEntityRepository,
                brandsRepository,
                cloudinaryHelperMock.Object);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "EditProduct command test")]
        [Fact(DisplayName = "Handle given invalid null should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new EditProductCommandHandler(
                this.deletableEntityRepository,
                It.IsAny<IDeletableEntityRepository<Brand>>(),
                It.IsAny<ICloudinaryHelper>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}