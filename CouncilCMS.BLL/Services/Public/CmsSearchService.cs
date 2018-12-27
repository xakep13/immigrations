using System;
using System.Collections.Generic;
using System.Linq;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Helpers;
using LemmaSharp;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsSearchService : BaseService
    {
        private IRepository<Page, int> pageRepo;
        private IRepository<Person, int> personRepo;
        private IRepository<Article, int> articleRepo;
        private IRepository<Enterprise, int> enterpriseRepo;
        private IRepository<CmsSearch, int> cmsSearchRepo;
        public CmsSearchService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsSearchService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            personRepo = UnitOfWork.GetIntRepository<Person>();
            articleRepo = UnitOfWork.GetIntRepository<Article>();
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            cmsSearchRepo = UnitOfWork.GetIntRepository<CmsSearch>();
        }

        public CmsSearchList Search(string query = null,  int page = 1, int perPage = 20)
        {
            var now = DateTime.Now;

            var count = 0;
           
            var ids = Find(query);

            var list = cmsSearchRepo.GetList(out count, x => ids.Contains(x.Id) && x.Date <= now && x.Published, x => x.OrderBy(o => o.Type).ThenByDescending(o => o.Date), null, (page - 1) * perPage, perPage).ToList();

            var model = new CmsSearchList()
            {
                Page = page,
                Count = count,
                Query = query,
                Results = new List<CmsSearchItem>()
            };

            foreach (var item in list)
            {
                switch (item.Type)
                {
                    case (int)CmsSearchType.Page:
                        {
                            var cmsPage = pageRepo.GetById(item.ItemId);

                            if (cmsPage != null)
                            {
                                model.Results.Add(new CmsSearchItem()
                                {
                                    Date = item.Date,
                                    Title = cmsPage.GetLocalValue("Title"),
                                    Type = "page",                                    
                                    Url = "/" + CurrentCulture.Name.ToLower() + "/page/" + cmsPage.UrlName
                                });
                            }
                            
                            break;
                        }
                    case (int)CmsSearchType.Person:
                        {
                            var cmsPerson = personRepo.GetById(item.ItemId);

                            if (cmsPerson != null)
                            {
                                model.Results.Add(new CmsSearchItem()
                                {
                                    Date = item.Date,
                                    Title = cmsPerson.GetLocalValue("Title"),
                                    Type = "page",
                                    Url = "/" + CurrentCulture.Name.ToLower() + "/persons/item/" + cmsPerson.Id + "/" + cmsPerson.UrlNameUk
                                });
                            }
                            
                            break;
                        }
                    case (int)CmsSearchType.Enterprise:
                        {
                            var cmsEnterprise = enterpriseRepo.GetById(item.ItemId);

                            if (cmsEnterprise != null)
                            {
                                model.Results.Add(new CmsSearchItem()
                                {
                                    Date = item.Date,
                                    Title = cmsEnterprise.GetLocalValue("Title"),
                                    Type = "page",
                                    Url = "/" + CurrentCulture.Name.ToLower() + "/organizations/item/" + cmsEnterprise.Id + "/" + cmsEnterprise.UrlNameUk
                                });
                            }

                            break;
                        }
                    case (int)CmsSearchType.Article:
                        {
                            var cmsArticle = articleRepo.GetById(item.ItemId);

                            if (cmsArticle != null)
                            {
                                model.Results.Add(new CmsSearchItem()
                                {
                                    Date = item.Date,
                                    Title = cmsArticle.GetLocalValue("Title"),
                                    Type = "page",
                                    Url = "/" + CurrentCulture.Name.ToLower() + "/articles/item/" + cmsArticle.Id + "/" + cmsArticle.UrlNameUk
                                });
                            }
                            
                            break;
                        }
                }
            }

            

            return model;
        }

        public List<Int32> Find(String query, CmsSearchType? type = null)
        {
            var lemmaProc = new LemmaProcessor(CurrentCulture.Name.ToLower());
            var against = lemmaProc.PrepareText(query);

            var ids = cmsSearchRepo.FullTextSearch("CleanText" + CurrentCulture.Name, against, "CmsSearches", 0, type != null ? ("Type = " + (int)type.Value) : null);
            
            return ids;
        }

        public List<Int32> GetItemIds(List<int> ids)
        {
            var now = DateTime.Now;
            var list = cmsSearchRepo.GetList(x => ids.Contains(x.Id) && x.Date <= now && x.Published).Select(x => x.ItemId).ToList();
           
            return list;
        }
    }
}
