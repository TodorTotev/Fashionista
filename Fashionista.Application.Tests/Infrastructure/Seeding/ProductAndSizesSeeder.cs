namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence;

    public class ProductAndSizesSeeder : ITestSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            dbContext.ProductColors.Add(new ProductColor
            {
                Name = "TestColor",
            });
            dbContext.SaveChanges();

            dbContext.ProductSizes.AddAsync(new ProductSize
            {
                Name = "TestSize",
                MainCategoryId = 1,
            });
        }
    }
}