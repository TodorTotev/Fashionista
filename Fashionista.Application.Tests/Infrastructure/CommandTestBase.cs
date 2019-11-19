namespace Fashionista.Application.Tests.Infrastructure
{
    using System;
    using Fashionista.Persistence;

    public class CommandTestBase : IDisposable
    {
        private readonly ApplicationDbContext db;

        public CommandTestBase(ApplicationDbContext db)
        {
            this.db = ApplicationDbContextFactory.Create();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(this.db);
        }
    }
}