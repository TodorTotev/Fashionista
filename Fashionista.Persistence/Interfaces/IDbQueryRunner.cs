namespace Fashionista.Persistence.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using Fashionista.Application.Interfaces;

    public interface IDbQueryRunner : IDisposable
    {
        Task RunQueryAsync(string query, params object[] parameters);
    }
}
