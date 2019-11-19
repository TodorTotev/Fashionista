namespace Fashionista.Persistence.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Interfaces;
    using Microsoft.EntityFrameworkCore;

    internal class SubCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!await dbContext.SubCategories.AnyAsync())
            {
                for (int i = 1; i <= 3; i++)
                {
                    var category = new SubCategory()
                    {
                        Name = "SubCategory" + i,
                        Description = "TestDescription",
                        MainCategoryId = 1,
                    };

                    await dbContext.SubCategories.AddAsync(category);
                }
            }
        }
    }
}
