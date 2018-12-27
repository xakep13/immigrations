using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Bissoft.CouncilCMS.Core
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity, TId>() where TEntity : class, IEntity<TId>;
        DbEntityEntry<TEntity> Entry<TEntity, TId>(TEntity entity) where TEntity : class, IEntity<TId>;
        IList<TElement> ExecuteStoredProcedureList<TElement>(string commandText, params object[] parameters) where TElement : class, new();
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters) where TElement : class, new();              
        int SaveChanges();
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        void Detach(object entity);
        bool ProxyCreationEnabled { get; set; }
        bool AutoDetectChangesEnabled { get; set; }        
        Database Database { get; }
    }
}

