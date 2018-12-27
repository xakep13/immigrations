using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Contexts;

namespace Bissoft.CouncilCMS.DAL.Repositories
{
    public class GenericRepository<TEntity, TId> : BaseRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        public GenericRepository(IDbContext context) : base(context) { }
    }
}
