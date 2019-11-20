namespace Fashionista.Application.Tests.Infrastructure
{
    using System;
    using System.Linq;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(DateTime.Now.ToString() + Guid.NewGuid().ToString())
                .ReplaceService<IModelCacheKeyFactory, CustomDynamicModelCacheKeyFactory>()
                .Options;

            var dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();

            dbContext.AddRange(new[]
            {
                new MainCategory { Name = "Category1" },
                new MainCategory { Name = "Category2" },
                new MainCategory { Name = "Category3" },
            });

            dbContext.SaveChangesAsync();

            for (int i = 1; i <= 3; i++)
            {
                var category = new SubCategory()
                {
                    Name = "Category" + i,
                    Description = "TestDesc",
                    MainCategoryId = 1,
                };

                dbContext.SubCategories.Add(category);
                dbContext.SaveChanges();
            }

            dbContext.SaveChanges();

            return dbContext;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void DetachAllEntities(ApplicationDbContext context)
        {
            var changedEntriesCopy = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted ||
                            e.State == EntityState.Unchanged)
                .ToList();

            foreach (var entry in changedEntriesCopy)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}