namespace Fashionista.Persistence.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Interfaces;
    using Microsoft.EntityFrameworkCore;

    internal class BrandSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!await dbContext.Brands.AnyAsync())
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
}
