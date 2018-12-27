using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;
using LemmaSharp;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsSearchAdminService : BaseService
    {
        private IRepository<CmsSearch, int> cmsSeacrhRepo;

        public CmsSearchAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsSearchAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            cmsSeacrhRepo = UnitOfWork.GetIntRepository<CmsSearch>();
        }       

        public List<Int32> Find(String query, CmsSearchType? type = null)
        {
            var lemmaProc = new LemmaProcessor(CurrentCulture.Name.ToLower());
            var against = lemmaProc.PrepareText(query);

            var ids = cmsSeacrhRepo.FullTextSearch("CleanText" + CurrentCulture.Name, against, null, 0, type != null ? ("Type = " + (int)type.Value) : null);

            var result = cmsSeacrhRepo.GetList(x => ids.Contains(x.Id)).Select(x => x.ItemId).ToList();

            return result;
        }

        public void Save(Int32 id, String textUk, string textRu, string textEn, DateTime? date, CmsSearchType type, Boolean published)
        {
            if (date.HasValue)
            {
                var item = cmsSeacrhRepo.GetSingle(x => x.ItemId == id && x.Type == (int)type) ?? new CmsSearch() { ItemId = id, Type = (int)type };

                var lemmatizeProcRu = new LemmaProcessor("ru");
                var lemmatizeProcUk = new LemmaProcessor("uk");
                var lemmatizeProcEn = new LemmaProcessor("en");

                item.CleanTextUk = lemmatizeProcUk.PrepareText(textUk);
                item.CleanTextRu = lemmatizeProcRu.PrepareText(textRu);
                item.CleanTextEn = lemmatizeProcRu.PrepareText(textEn);
                item.Date = date.Value;
                item.Published = published;

                cmsSeacrhRepo.InsertOrUpdate(item);
                UnitOfWork.Commit();
            }
        }

        public void Delete(Int32 id, CmsSearchType type)
        {
            var item = cmsSeacrhRepo.GetSingle(x => x.ItemId == id && x.Type == (int)type);

            if (item != null)
            {
                item.Deleted = true;

                cmsSeacrhRepo.Update(item);

                UnitOfWork.Commit();
            }
        }

        public void Restore(Int32 id, CmsSearchType type)
        {
            var item = cmsSeacrhRepo.GetSingle(x => x.ItemId == id && x.Type == (int)type);

            if (item != null)
            {
                item.Deleted = false;

                cmsSeacrhRepo.Update(item);

                UnitOfWork.Commit();
            }
        }

        public void Remove(Int32 id, CmsSearchType type)
        {
            var item = cmsSeacrhRepo.GetSingle(x => x.ItemId == id && x.Type == (int)type);

            if (item != null)
            {
                cmsSeacrhRepo.Delete(item);

                UnitOfWork.Commit();
            }
        }

        private List<CheckedListItem> GetCategoryCheckedList(List<CheckedListItem> targetList, List<ArticleCategory> levelList, List<ArticleCategory> sourceList, int level = 0)
        {
            targetList = targetList ?? new List<CheckedListItem>();

            foreach (var cat in levelList)
            {
                targetList.Add(new CheckedListItem() { Value = cat.Id, Name = cat.GetLocalValue("Title"), Level = level });

                if (sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
                {
                    GetCategoryCheckedList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1);
                }
            }

            return targetList;
        }
    }
}