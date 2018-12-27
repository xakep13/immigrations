using System;
using System.Data.Entity;
using Bissoft.CouncilCMS.DAL.Contexts;
using Bissoft.CouncilCMS.DAL.Repositories;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;
        private IDbContext context;

        public UnitOfWork(IDbContext context)
        {
            disposed = false;
            this.context = context;
        }

        public UnitOfWork(string connectionString)
        {
            disposed = false;
            this.context = new CmsDbContext(connectionString);            
        }
        
        public void DefineAsDeleted<TEntity, TId>(TEntity Element) where TEntity : class, IEntity<TId>
        {
            context.Entry<TEntity, TId>(Element).State = EntityState.Deleted;
        }

        public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : class, IEntity<TId>
        {
            return new GenericRepository<TEntity, TId>(context);
        }

        public IRepository<TEntity, int> GetIntRepository<TEntity>() where TEntity : class, IEntity<int>
        {
            return new IntGenericRepository<TEntity>(context);
        }

        public TRepository GetStraightRepository<TRepository, TEntity>() where TRepository : class, IRepository<TEntity, int> where TEntity : class, IEntity<int>
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository), new object[] { context });
        }

        public IDbContext Context
        {
            get { return this.context; }
        }

        public void Commit()
        {
            context.SaveChanges();
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
