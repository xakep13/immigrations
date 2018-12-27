using System;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.DAL.Contexts;
using System.Linq.Expressions;
using Bissoft.CouncilCMS.DAL.Entities;
using xTab.Tools.Helpers;
using System.Linq;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public abstract class BaseService : IService
    {
        private IUnitOfWork unitOfWork;

        public BaseService()
        {
            this.unitOfWork = new UnitOfWork(new CmsDbContext(true));
        }

        public BaseService(String ConnectionString)
        {
            this.unitOfWork = new UnitOfWork(new CmsDbContext(ConnectionString, true));
        }

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return unitOfWork; }
        }

        protected Expression<Func<TEntity, bool>> TitleFilter<TEntity>(Expression<Func<TEntity, bool>> predicate, string value) where TEntity : BaseAuditionEntity
        {
            var id = 0;

            if (!String.IsNullOrEmpty(value))
            {
                if (int.TryParse(value, out id))
                    predicate = predicate.And(x => x.Id == id || x.TitleRu.Contains(value) || x.TitleUk.Contains(value) || x.TitleEn.Contains(value) );
                else
                    predicate = predicate.And(x => x.TitleRu.Contains(value) || x.TitleUk.Contains(value) || x.TitleEn.Contains(value));
            }

            return predicate;
        }

        protected Expression<Func<TEntity, bool>> DateRangeFilter<TEntity>(Expression<Func<TEntity, bool>> predicate, string value, int dateType = 0) where TEntity : BaseAuditionEntity
        {
            var dates = value.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

            if (dates.Length == 2)
            {
                var fromDate = DateTimeHelper.ParseDateNullable(dates[0]);
                var toDate = DateTimeHelper.ParseDateNullable(dates[1]);

                if (fromDate != null && toDate != null)
                {
                    fromDate = fromDate.Value.Date;
                    toDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);

                    switch (dateType)
                    {
                        case 0:
                            predicate = predicate.And(x => x.CreateDate >= fromDate && x.CreateDate <= toDate);
                            break;
                        case 1:
                            predicate = predicate.And(x => x.PublishDate >= fromDate && x.PublishDate <= toDate);
                            break;
                        default:
                            goto case 1;
                    }
                }
            }

            return predicate;
        }

        protected Expression<Func<TEntity, bool>> UserFilter<TEntity>(Expression<Func<TEntity, bool>> predicate, int? value, int userAction = 0) where TEntity : BaseAuditionEntity
        {
            if (userAction == 0)
                predicate = predicate.And(x => x.CreateUserId == value);
            else
                predicate = predicate.And(x => x.LastEditUserId == value);

            return predicate;
        }

        protected Expression<Func<TEntity, bool>> CreateStatePredicate<TEntity>(CmsItemState value) where TEntity : BaseAuditionEntity
        {
            var predicate = PredicateBuilder.True<TEntity>();

            switch (value)
            {
                case CmsItemState.Published:
                    predicate = predicate.And(x => x.Published && !x.Deleted);
                    break;
                case CmsItemState.NotPublished:
                    predicate = predicate.And(x => !x.Published && !x.Deleted);
                    break;
                case CmsItemState.Deleted:
                    predicate = predicate.And(x => x.Deleted);
                    break;
            }

            return predicate;
        }
    }
}
