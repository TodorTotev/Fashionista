namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence;

    public class MainCategorySeeder : ITestSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            dbContext.AddRange(new[]
            {
                new MainCategory { Name = "Category1" },
                new MainCategory { Name = "Category2" },
                new MainCategory { Name = "Category3" },
            });
        }
    }
}