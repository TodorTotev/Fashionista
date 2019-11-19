namespace Fashionista.Application.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fashionista.Domain.Infrastructure;
    using Fashionista.Persistence.Interfaces;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
