namespace Fashionista.Persistence.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Common;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class MainCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
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
