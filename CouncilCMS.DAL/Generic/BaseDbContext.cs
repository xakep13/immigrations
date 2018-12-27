using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Bissoft.CouncilCMS.Core;

namespace Bissoft.CouncilCMS.DAL.Contexts
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(Boolean disableInitializer = true) : base(Properties.Settings.Default.ConnectionString)
        {
            if (disableInitializer)
            {
                Database.SetInitializer<BaseDbContext>(null);
            }
        }

        public BaseDbContext(String ConnectionString, Boolean disableInitializer = true) : base(ConnectionString)
        {
            if (disableInitializer)
            {
                Database.SetInitializer<BaseDbContext>(null);
            }
        }

       
        public DbSet<TEntity> Set<TEntity, TId>() where TEntity : class, IEntity<TId>
        {
            return base.Set<TEntity>();
        }

        public DbEntityEntry<TEntity> Entry<TEntity, TId>(TEntity entity) where TEntity : class, IEntity<TId>
        {
            return base.Entry<TEntity>(entity);
        }

        public IList<TElement> ExecuteStoredProcedureList<TElement>(string commandText, params object[] parameters) where TElement : class, new()
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;

                    if (p == null)
                    {
                        throw new Exception("Not support parameter type");
                    }
                    
                    commandText += i == 0 ? " " : ", ";
                    commandText += "@" + p.ParameterName;

                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TElement>(commandText, parameters).ToList();
            
            return result;
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters) where TElement : class, new()
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }
    
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;

            if (timeout.HasValue)
            {
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            return result;
        }

        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("null entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }        

        public virtual bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        public virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public new Database Database
        {
            get
            {
                return base.Database;
            }
        }
    }
}
