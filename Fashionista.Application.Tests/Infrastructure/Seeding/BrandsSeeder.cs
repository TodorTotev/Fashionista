namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence;

    public class BrandsSeeder : ITestSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            dbContext.Brands.Add(new Brand
            {
                Name = "TestBrand",
                BrandPhotoUrl = "https://imgur.com/uyT0KJ8",
            });
        }
    }
}