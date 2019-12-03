namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using Fashionista.Persistence;

    public interface ITestSeeder
    {
        void Seed(ApplicationDbContext dbContext);
    }
}