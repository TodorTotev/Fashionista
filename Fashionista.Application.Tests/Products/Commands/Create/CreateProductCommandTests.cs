namespace Fashionista.Application.Tests.Products.Commands.Create
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Fashionista.Application.Interfaces;
    using Fashionista.Application.Products.Commands.Create;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Persistence.Repositories;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CreateProductCommandTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "Create product command tests")]
        [Fact(DisplayName = "Handle given valid request should create product")]
        public async Task Handle_GivenValidRequest_ShouldCreateProduct()
        {
            // Arrange
            var cloudinaryHelperMock = new Mock<ICloudinaryHelper>();
            cloudinaryHelperMock
                .Setup(x => x.UploadImage(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<Transformation>()));

            var command = new CreateProductCommand
            {
                Name = "ValidName",
                Description = "ValidDescription",
                Photos = It.IsAny<ICollection<IFormFile>>(),
                BrandId = 1,
                Price = 120,
                ProductType = ProductType.Men,
                SubCategoryId = 1,
            };

            var brandsRepository = new EfDeletableEntityRepository<Brand>(this.dbContext);
            var sut = new CreateProductCommandHandler(
                this.deletableEntityRepository,
                brandsRepository,
                this.mapper,
                cloudinaryHelperMock.Object);

            // Act
            var id = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            var createdProduct = this.deletableEntityRepository
                .AllAsNoTracking()
                .SingleOrDefault(x => x.Name == "ValidName");

            id.ShouldBe(3);
            createdProduct.Name.ShouldBe("ValidName");
            createdProduct.Description.ShouldBe("ValidDescription");
            createdProduct.Price.ShouldBe(120);
            createdProduct.BrandId.ShouldBe(1);
            createdProduct.ProductType.ShouldBe(ProductType.Men);
            createdProduct.SubCategoryId.ShouldBe(1);
        }
    }
}