using Bissoft.CouncilCMS.Core;
using System.Linq;

namespace Bissoft.CouncilCMS.DAL.Repositories
{
    public class IntGenericRepository<TEntity> : BaseRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
        public IntGenericRepository(IDbContext context) :base(context) { }

        public override void InsertOrUpdate(TEntity element)
        {
            if (element.Id > 0)
                Update(element);
            else
                Insert(element);
        }

        public override TEntity GetById(int id, string includeProperties = null, bool asNoTracking = false, bool notDeleted = false)
        {
            if (string.IsNullOrEmpty(includeProperties) && !asNoTracking && !notDeleted)
            {
                return base.GetById(id, includeProperties, asNoTracking, notDeleted);
            }
            else
            {
                var count = 0;
                var item = GetQueryable(out count, x => x.Id == id, includeProperties: includeProperties, asNoTracking: asNoTracking, notDeleted: notDeleted, calculateCount: false).FirstOrDefault();
                return item;
            }            
        }
    }
}
