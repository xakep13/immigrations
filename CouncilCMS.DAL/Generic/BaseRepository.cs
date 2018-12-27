using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        protected IDbContext context;
        protected DbSet<TEntity> dbSet;

        public BaseRepository(IDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity, TId>();
        }


        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = default(int?), int? take = default(int?), bool asNoTracking = true, bool notDeleted = true)
        {
            var count = 0;

            var query = GetQueryable(out count, filter, orderBy, includeProperties, skip, take, asNoTracking, notDeleted);

            return query;
        }

        public virtual IEnumerable<TEntity> GetList(out int count, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = default(int?), int? take = default(int?), bool asNoTracking = true, bool notDeleted = true)
        {
            var query = GetQueryable(out count, filter, orderBy, includeProperties, skip, take, asNoTracking, notDeleted, true);

            return query;
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null, bool asNoTracking = false, bool notDeleted = false)
        {
            var count = 0;

            var query = GetQueryable(out count, filter: filter, includeProperties: includeProperties, asNoTracking: asNoTracking, notDeleted: notDeleted);

            return query.FirstOrDefault();
        }

        public virtual TEntity GetById(TId id, string includeProperties = null, bool asNoTracking = false, bool notDeleted = false)
        {
            var item = dbSet.Find(id);

            return item;
        }

        public virtual IQueryable<TEntity> GetQuery(string includeProperties = null)
        {
            var query = CreateQuery(includeProperties);

            return query;
        }

        public virtual List<TId> FullTextSearch(string match, string against, string dbName = null, int limit = 0, string AdditionalWhereCondition = null)
        {
            if (string.IsNullOrEmpty(against) == false)
            {
                dbName = dbName ?? typeof(TEntity).Name + "s";
                var query = String.Format(
					@"SELECT Id FROM 
						(SELECT Id, MATCH ({0}) AGAINST (@p0 IN NATURAL LANGUAGE MODE) AS score 
							FROM {1} as tbl1 WHERE {2} MATCH({0}) 
							AGAINST (@p0 IN BOOLEAN MODE) 
						ORDER BY score DESC) as result;", 
					match, dbName, !String.IsNullOrEmpty(AdditionalWhereCondition) ? (AdditionalWhereCondition + " AND ") : String.Empty);
                List<TId> list;
                IEnumerable<TId> listProto;

                listProto = context.Database.SqlQuery<TId>(query, "+" + against.Replace(" ", " +"));//.ToList();

                if (listProto.Any() == true)
                {
                    list = listProto.ToList();
                }
                else
                {
                    list = new List<TId>();
                }
                return list;
            }
            else
            {
                return new List<TId>();
            }
        }

        public virtual void InsertOrUpdate(TEntity element)
        {
            if (element.Id != null)
                Update(element);
            else
                Insert(element);
        }

        public virtual void Insert(TEntity element)
        {
            dbSet.Add(element);
        }

        public virtual void Update(TEntity element)
        {
            dbSet.Attach(element);
            context.Entry<TEntity, TId>(element).State = EntityState.Modified;
        }

        public virtual void Delete(TId id)
        {
            TEntity element = context.Set<TEntity, TId>().Find(id);
            Delete(element);
        }

        public virtual void Delete(TEntity element)
        {
            if (context.Entry<TEntity, TId>(element).State == EntityState.Detached)
                dbSet.Attach(element);

            dbSet.Remove(element);
        }

        public virtual void ExecuteCommand(string sqlQuery)
        {
            context.ExecuteSqlCommand(sqlQuery);
        }

        public virtual void Commit()
        {
            context.SaveChanges();
        }

        public DbSet<TEntity> DbSet { get { return this.dbSet; } }

        public IDbContext Context { get { return this.context; } }

        protected virtual IQueryable<TEntity> CreateQuery(String Includes = null)
        {
            var query = dbSet.AsQueryable();

            if (!String.IsNullOrEmpty(Includes))
                foreach (var property in Includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(property);

            return query;
        }

        protected IQueryable<TEntity> GetQueryable(out Int32 count, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = default(int?), int? take = default(int?), bool asNoTracking = false, bool notDeleted = true, bool calculateCount = false)
        {
            var query = CreateQuery(includeProperties);

            if (notDeleted)
                query = query.Where(x => !x.Deleted);

            if (filter != null)
                query = query.Where(filter);

            if (calculateCount)
                count = query.Count();
            else
                count = 0;

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}
