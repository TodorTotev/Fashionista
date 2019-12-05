namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using System.Collections.Generic;
    using Fashionista.Domain.Entities;
    using Fashionista.Domain.Entities.Enums;
    using Fashionista.Persistence;

    internal class ProductSeeder : ITestSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            var product = dbContext.Products.Add(new Product
            {
                Name = "ActiveProduct",
                Description = "TestDesc",
                Price = 100,
                BrandId = 1,
                IsHidden = false,
                Photos = new List<string>(),
                Reviews = new List<Review>(),
                ProductType = ProductType.Men,
                SubCategoryId = 1,
            });

            dbContext.SaveChanges();

            product.Entity.Photos.Add("TestUrl");

            dbContext.Add(new Domain.Entities.ProductAttributes
            {
                Quantity = 1,
                ProductSizeId = 1,
                ProductColorId = 1,
                ProductId = 1,
            });

            product.Entity.Reviews.Add(new Review
            {
                Rating = 5,
            });

            dbContext.Products.Add(new Product
            {
                Name = "DraftProduct",
                Description = "TestDesc",
                Price = 100,
                BrandId = 1,
                IsHidden = true,
                Photos = new List<string>(),
                Reviews = new List<Review>(),
                ProductType = ProductType.Women,
                SubCategoryId = 1,
            });

            dbContext.OrderProducts.Add(new OrderProduct
            {
                ProductId = product.Entity.Id,
                OrderId = 1,
                Quantity = 5,
                Product = product.Entity,
            });
        }
    }
}