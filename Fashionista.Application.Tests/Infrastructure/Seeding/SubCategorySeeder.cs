namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence;

    public class SubCategorySeeder : ITestSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            for (int i = 1; i <= 3; i++)
            {
                var category = new SubCategory()
                {
                    Name = "Category" + i,
                    Description = "TestDesc",
                    MainCategoryId = 1,
                };

                dbContext.SubCategories.Add(category);
            }
        }
    }
}