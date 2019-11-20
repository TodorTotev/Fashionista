namespace Fashionista.Persistence.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Interfaces;
    using Microsoft.EntityFrameworkCore;

    internal class MainCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!await dbContext.MainCategories.AnyAsync())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var category = new MainCategory
                    {
                        Name = "Category" + i,
                    };

                    await dbContext.MainCategories.AddAsync(category);
                }
            }
        }
    }
}
