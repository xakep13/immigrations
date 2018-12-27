using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Bissoft.CouncilCMS.Core
{
    public interface IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        IQueryable<TEntity> GetQuery(string includeProperties = null);
        IEnumerable<TEntity> GetList(out int count, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = true, bool notDeleted = true);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = true, bool notDeleted = true);
        TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, bool asNoTracking = false, bool notDeleted = false);
        TEntity GetById(TId id, string includeProperties = null, bool asNoTracking = false, bool notDeleted = false);
        List<TId> FullTextSearch(string match, string against, string dbName = null, int limit = 0, string additionlWhereCondition = null);
        void ExecuteCommand(String sqlQuery);
        void InsertOrUpdate(TEntity element);
        void Insert(TEntity element);
        void Update(TEntity element);
        void Delete(TEntity element);
        void Delete(TId id);

        DbSet<TEntity> DbSet { get; }
        IDbContext Context { get; }
    }
}
