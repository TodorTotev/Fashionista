namespace Fashionista.Persistence.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Interfaces;

    internal class BrandSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await dbContext.Brands.AddAsync(new Brand
            {
                Name = "TestBrand",
                BrandPhotoUrl = "https://imgur.com/uyT0KJ8",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
