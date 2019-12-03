// ReSharper disable PossibleNullReferenceException

using System;

namespace Fashionista.Application.Tests.Products.Commands.Sort
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fashionista.Application.Common.Models;
    using Fashionista.Application.Exceptions;
    using Fashionista.Application.Products.Commands.Sort;
    using Fashionista.Application.Tests.Infrastructure;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Persistence.Repositories;
    using Moq;
    using Shouldly;
    using Xunit;

    public class SortProductsCommandTests : BaseTest<Product>
    {
        [Trait(nameof(Product), "SortProducts command tests")]
        [Fact(DisplayName = "Handle given valid request should return sorted products")]
        public async Task Handle_GivenValidRequest_ShouldReturnSortedProducts()
        {
            // Arrange
            var dummyList = new List<ProductLookupModel>();
            this.dbContext.Products.Where(x => x.SubCategoryId == 1)
                .Select(x => new ProductLookupModel
                {
                    Name = x.Name,
                    SubCategoryId = x.SubCategoryId,
                    ProductAttributes = x.ProductAttributes,
                    ProductType = x.ProductType,
                    BrandId = x.BrandId,
                })
                .ToList()
                .ForEach(x => dummyList.Add(x));

            var sizesRepository = new EfDeletableEntityRepository<ProductSize>(this.dbContext);
            var colorsRepository = new EfDeletableEntityRepository<ProductColor>(this.dbContext);

            var command = new SortProductsCommand
            {
                BrandId = 1,
                ColorId = 1,
                SizeId = 1,
                Sort = ProductSort.Newest,
                Gender = ProductType.Men,
                Products = dummyList,
            };

            var sut = new SortProductsCommandHandler(sizesRepository, colorsRepository);

            // Act
            var list = await sut.Handle(command, It.IsAny<CancellationToken>());

            // Assert
            list.ShouldNotBeNull();
            list.Count.ShouldBe(1);
            var product = list.FirstOrDefault();
            product.BrandId.ShouldBe(1);
            product.ProductType.ShouldBe(ProductType.Men);
            product.ProductAttributes.FirstOrDefault().ProductColorId.ShouldBe(1);
            product.ProductAttributes.FirstOrDefault().ProductSizeId.ShouldBe(1);
        }

        [Trait(nameof(Product), "SortProducts command tests")]
        [Fact(DisplayName = "Handle given invalid ColorId should throw NotFoundException")]
        public async Task Handle_GivenInvalidColorId_ShouldThrowNotFoundException()
        {
            // Arrange
            var dummyList = new List<ProductLookupModel>();
            this.dbContext.Products.Where(x => x.SubCategoryId == 1)
                .Select(x => new ProductLookupModel
                {
                    Name = x.Name,
                    SubCategoryId = x.SubCategoryId,
                    ProductAttributes = x.ProductAttributes,
                    ProductType = x.ProductType,
                    BrandId = x.BrandId,
                })
                .ToList()
                .ForEach(x => dummyList.Add(x));

            var sizesRepository = new EfDeletableEntityRepository<ProductSize>(this.dbContext);
            var colorsRepository = new EfDeletableEntityRepository<ProductColor>(this.dbContext);

            var command = new SortProductsCommand
            {
                BrandId = 1,
                ColorId = 2,
                SizeId = 1,
                Sort = ProductSort.Newest,
                Gender = ProductType.Men,
                Products = dummyList,
            };

            var sut = new SortProductsCommandHandler(sizesRepository, colorsRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "SortProducts command tests")]
        [Fact(DisplayName = "Handle given invalid SizeId should throw NotFoundException")]
        public async Task Handle_GivenInvalidSizeId_ShouldThrowNotFoundException()
        {
            // Arrange
            var dummyList = new List<ProductLookupModel>();
            this.dbContext.Products.Where(x => x.SubCategoryId == 1)
                .Select(x => new ProductLookupModel
                {
                    Name = x.Name,
                    SubCategoryId = x.SubCategoryId,
                    ProductAttributes = x.ProductAttributes,
                    ProductType = x.ProductType,
                    BrandId = x.BrandId,
                })
                .ToList()
                .ForEach(x => dummyList.Add(x));

            var sizesRepository = new EfDeletableEntityRepository<ProductSize>(this.dbContext);
            var colorsRepository = new EfDeletableEntityRepository<ProductColor>(this.dbContext);

            var command = new SortProductsCommand
            {
                BrandId = 1,
                ColorId = 1,
                SizeId = 2,
                Sort = ProductSort.Newest,
                Gender = ProductType.Men,
                Products = dummyList,
            };

            var sut = new SortProductsCommandHandler(sizesRepository, colorsRepository);

            // Act & Assert
            await Should.ThrowAsync<NotFoundException>(sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Trait(nameof(Product), "SortProducts command tests")]
        [Fact(DisplayName = "Handle given null request should throw ArgumentNullException")]
        public async Task Handle_GivenNullRequest_ShouldThrowArgumentNullException()
        {
            // Arrange
            var sut = new SortProductsCommandHandler(
                It.IsAny<EfDeletableEntityRepository<ProductSize>>(),
                It.IsAny<EfDeletableEntityRepository<ProductColor>>());

            // Act & Assert
            await Should.ThrowAsync<ArgumentNullException>(sut.Handle(null, It.IsAny<CancellationToken>()));
        }
    }
}