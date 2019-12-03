namespace Fashionista.Application.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fashionista.Application.Tests.Infrastructure.Seeding;
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

            Seed(dbContext);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static void Seed(ApplicationDbContext dbContext)
        {
            var seeders = new List<ITestSeeder>
            {
                new MainCategorySeeder(),
                new SubCategorySeeder(),
                new BrandsSeeder(),
                new ProductAndSizesSeeder(),
                new ProductSeeder(),
                new UserSeeder(),
            };

            foreach (var seeder in seeders)
            {
                seeder.Seed(dbContext);
                dbContext.SaveChangesAsync();
            }
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