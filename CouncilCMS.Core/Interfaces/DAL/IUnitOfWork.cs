using System;
using System.Data.Entity;

namespace Bissoft.CouncilCMS.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class, IEntity<TId>;
        IRepository<TEntity, int> GetIntRepository<TEntity>() where TEntity : class, IEntity<int>;
        TRepository GetStraightRepository<TRepository, TEntity>() where TRepository : class, IRepository<TEntity, int> where TEntity : class, IEntity<int>;
        IDbContext Context { get; }

        void DefineAsDeleted<TEntity, TId>(TEntity Element) where TEntity : class, IEntity<TId>;
        void Commit();
        void Dispose(bool disposing);
    }
}
