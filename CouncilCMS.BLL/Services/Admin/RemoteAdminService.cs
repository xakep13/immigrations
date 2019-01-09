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

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class RemoteAdminService : BaseService
    {        
        private IRepository<Page, int> pageRepo;
        private IRepository<Person, int> personRepo;
        private IRepository<Article, int> articleRepo;
        private IRepository<DamagedHousing, int> damagedHousingRepo;
		private IRepository<Enterprise, int> enterpriseRepo;
        private IRepository<Doc, int> docRepo;
        private IRepository<PersonCategory, int> personCategoryRepo;
        private IRepository<ArticleCategory, int> articleCategoryRepo;
        private IRepository<DamagedHousingCategory, int> damagedHousingCategoryRepo;
		private IRepository<EnterpriseCategory, int> enterpriseCategoryRepo;
        private IRepository<DocCategory, int> docCategoryRepo;        
        
        public RemoteAdminService(string ConnectionString) : base(ConnectionString) { }
        public RemoteAdminService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<SuggestionItem> FindPage(String Query, String excludeIds = null, Boolean OnlyPublished = true, Boolean OnlyAvailable = true)
        {
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            
            var predicate = PredicateBuilder.True<Page>();
            var id = 0;

            if (OnlyPublished)
                predicate = predicate.And(x => x.Published);
            
            if (Int32.TryParse(Query, out id))
                predicate = predicate.And(x => (x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query) || x.Id == id));
            else
                predicate = predicate.And(x => (x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query)));
            
            var list = pageRepo.GetList(predicate, x => x.OrderByColumn(CurrentCulture.PropertyName("Title")), null, null, null, true, OnlyAvailable)
                .Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk, TitleEn = x.TitleEn })
                .ToList()
                .Select(x => new SuggestionItem()
                {
                    Value = x.Id.ToString(),
                    DisplayText = x.GetLocalValue("Title") + " [id: " + x.Id + "]",
                    ValueText = x.GetLocalValue("Title") + " [id: " + x.Id + "]"
                }).ToList();

            return list;
        }

        public List<SuggestionItem> FindArticle(String Query, String excludeIds = null, Boolean OnlyPublished = true, Boolean OnlyAvailable = true)
        {
            var articleRepo = UnitOfWork.GetIntRepository<Article>();

            var predicate = PredicateBuilder.True<Article>();
            var id = 0;

            if (OnlyPublished)
                predicate = predicate.And(x => x.Published);
          
            if (Int32.TryParse(Query, out id))
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query) || x.Id == id));
            else
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query)));

            var list = articleRepo.GetList(predicate, x => x.OrderByColumn(CurrentCulture.PropertyName("Title")), null, null, null, true, OnlyAvailable)
                .Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk, TitleEn = x.TitleEn })
                .ToList()
                .Select(x => new SuggestionItem()
                {
                    Value = x.Id.ToString(),
                    DisplayText = x.GetLocalValue("Title") + " [id: " + x.Id + "]",
                    ValueText = x.GetLocalValue("Title") + " [id: " + x.Id + "]"
                }).ToList();

            return list;
        }

		public List<SuggestionItem> FindDamagedHousing(String Query, String excludeIds = null, Boolean OnlyPublished = true, Boolean OnlyAvailable = true)
		{
			var articleRepo = UnitOfWork.GetIntRepository<DamagedHousing>();

			var predicate = PredicateBuilder.True<DamagedHousing>();
			var id = 0;

			if(OnlyPublished)
				predicate = predicate.And(x => x.Published);

			if(Int32.TryParse(Query, out id))
				predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query) || x.Id == id));
			else
				predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query)));

			var list = articleRepo.GetList(predicate, x => x.OrderByColumn(CurrentCulture.PropertyName("Title")), null, null, null, true, OnlyAvailable)
				.Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk, TitleEn = x.TitleEn })
				.ToList()
				.Select(x => new SuggestionItem()
				{
					Value = x.Id.ToString(),
					DisplayText = x.GetLocalValue("Title") + " [id: " + x.Id + "]",
					ValueText = x.GetLocalValue("Title") + " [id: " + x.Id + "]"
				}).ToList();

			return list;
		}

		public List<SuggestionItem> FindPerson(String Query, String excludeIds = null, Boolean OnlyPublished = true, Boolean OnlyAvailable = true)
        {
            var personRepo = UnitOfWork.GetIntRepository<Person>();

            var predicate = PredicateBuilder.True<Person>();
            var id = 0;

            if (OnlyPublished)
                predicate = predicate.And(x => x.Published);
         
            if (Int32.TryParse(Query, out id))
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query) || x.Id == id));
            else
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query)));

            var list = personRepo.GetList(predicate, x => x.OrderByColumn(CurrentCulture.PropertyName("Title")), null, null, null, true, OnlyAvailable)
                .Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk, TitleEn = x.TitleEn })
                .ToList()
                .Select(x => new SuggestionItem()
                {
                    Value = x.Id.ToString(),
                    DisplayText = x.GetLocalValue("Title") + " [id: " + x.Id + "]",
                    ValueText = x.GetLocalValue("Title") + " [id: " + x.Id + "]"
                }).ToList();

            return list;
        }

        public List<SuggestionItem> FindDoc(String Query, string category = null, String excludeIds = null, Boolean OnlyPublished = true, Boolean OnlyAvailable = true)
        {
            var docRepo = UnitOfWork.GetIntRepository<Doc>();

            var predicate = PredicateBuilder.True<Doc>();
            var id = 0;

            if (OnlyPublished)
                predicate = predicate.And(x => x.Published);  

            if (Int32.TryParse(Query, out id))
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query) || x.Id == id));
            else
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query)));

            if (!String.IsNullOrEmpty(category))
                predicate = predicate.And(x => x.Categories.Any(c => c.UrlName == category));

            var list = docRepo.GetList(predicate, x => x.OrderByColumn(CurrentCulture.PropertyName("Title")), "Categories", 0, 50, true, OnlyAvailable)
                .Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk, TitleEn = x.TitleEn })
                .ToList()
                .Select(x => new SuggestionItem()
                {
                    Value = x.Id.ToString(),
                    DisplayText = x.GetLocalValue("Title") + " [id: " + x.Id + "]",
                    ValueText = x.GetLocalValue("Title") + " [id: " + x.Id + "]"
                }).ToList();

            return list;
        }

        public List<SuggestionItem> FindEnterprise(String Query, String excludeIds = null, Boolean OnlyParents = true, Boolean OnlyPublished = true, Boolean OnlyAvailable = true, Int32? userId = null)
        {
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();

            var predicate = PredicateBuilder.True<Enterprise>();
            var id = 0;

            if (OnlyParents)
                predicate = predicate.And(x => x.ParentEnterpriseId == null);

            if (OnlyPublished)
                predicate = predicate.And(x => x.Published);

            if (userId > 0)
                predicate = predicate.And(x => x.AllowedUsers.Any(u => u.Id == userId.Value));

            if (Int32.TryParse(Query, out id))
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query) || x.Id == id));
            else
                predicate = predicate.And(x => (x.TitleEn.Contains(Query) || x.TitleRu.Contains(Query) || x.TitleUk.Contains(Query)));

            var list = enterpriseRepo.GetList(predicate, x => x.OrderByColumn(CurrentCulture.PropertyName("Title")), "AllowedUsers", null, null, true, OnlyAvailable)
                .Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk, TitleEn = x.TitleEn })
                .ToList()
                .Select(x => new SuggestionItem()
                {
                    Value = x.Id.ToString(),
                    DisplayText = x.GetLocalValue("Title") + " [id: " + x.Id + "]",
                    ValueText = x.GetLocalValue("Title") + " [id: " + x.Id + "]"
                }).ToList();

            return list;
        }

        public List<SuggestionItem> FindCmsLink(String query, Boolean OnlyAvailable = true)
        {
            articleRepo = UnitOfWork.GetIntRepository<Article>();
			damagedHousingRepo = UnitOfWork.GetIntRepository<DamagedHousing>();
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            personRepo = UnitOfWork.GetIntRepository<Person>();
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            personCategoryRepo = UnitOfWork.GetIntRepository<PersonCategory>();
            articleCategoryRepo = UnitOfWork.GetIntRepository<ArticleCategory>();
            damagedHousingCategoryRepo = UnitOfWork.GetIntRepository<DamagedHousingCategory>();
			enterpriseCategoryRepo = UnitOfWork.GetIntRepository<EnterpriseCategory>();
            docCategoryRepo = UnitOfWork.GetIntRepository<DocCategory>();

            var model = new List<SuggestionItem>();
            var id = 0;

            var pagePredicate = PredicateBuilder.True<Page>();
            var personPredicate = PredicateBuilder.True<Person>();
            var articlePredicate = PredicateBuilder.True<Article>();
            var damagedHousingPredicate = PredicateBuilder.True<DamagedHousing>();
			var enterprisePredicate = PredicateBuilder.True<Enterprise>();
            var docPredicate = PredicateBuilder.True<Doc>();
            var articleCategoryPredicate = PredicateBuilder.True<ArticleCategory>();
            var damagedHousingCategoryPredicate = PredicateBuilder.True<DamagedHousingCategory>();
			var personCategoryPredicate = PredicateBuilder.True<PersonCategory>();
            var enterpriseCategoryPredicate = PredicateBuilder.True<EnterpriseCategory>();
            var docCategoryPredicate = PredicateBuilder.True<DocCategory>();

            if (Int32.TryParse(query, out id))
            {
                pagePredicate = pagePredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query) || x.Id == id));
                personPredicate = personPredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query) || x.Id == id));
                enterprisePredicate = enterprisePredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query) || x.Id == id));
                docPredicate = docPredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query) || x.Id == id));
                articlePredicate = articlePredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query) || x.Id == id));
                damagedHousingPredicate = damagedHousingPredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query) || x.Id == id));
				articleCategoryPredicate = articleCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query) || x.Id == id));
				damagedHousingCategoryPredicate = damagedHousingCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query) || x.Id == id));
				personCategoryPredicate = personCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query) || x.Id == id));
                enterpriseCategoryPredicate = enterpriseCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query) || x.Id == id));
                docCategoryPredicate = docCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query) || x.Id == id));
            }
            else
            {
                pagePredicate = pagePredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query)));
                personPredicate = personPredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query)));
                enterprisePredicate = enterprisePredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query)));
                docPredicate = docPredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query)));
                articlePredicate = articlePredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query)));
				damagedHousingPredicate = damagedHousingPredicate.And(x => (x.TitleRu.Contains(query) || x.TitleUk.Contains(query)));
				articleCategoryPredicate = articleCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query)));
				damagedHousingCategoryPredicate = damagedHousingCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query)));
				personCategoryPredicate = personCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query)));
                enterpriseCategoryPredicate = enterpriseCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query)));
                docCategoryPredicate = docCategoryPredicate.And(x => (x.TitleUk.Contains(query) || x.TitleRu.Contains(query)));
            }

            model.AddRange(pageRepo.GetList(pagePredicate)
                .Select(x => new SuggestionItem()
                {
                    Value = "/page/" + x.UrlName,
                    DisplayText = "[сторінка] - " + x.TitleUk,
                    ValueText = "[сторінка] - " + x.TitleUk,
                }).ToList());

            model.AddRange(articleRepo.GetList(articlePredicate)
                .Select(x => new SuggestionItem()
                {
                    Value = "/articles/item/" + x.Id.ToString() + "/" + x.UrlNameUk,
                    DisplayText = "[матеріал] - " + x.TitleUk,
                    ValueText = "[матеріал] - " + x.TitleUk,
                }).ToList());

			model.AddRange(damagedHousingRepo.GetList(damagedHousingPredicate)
				.Select(x => new SuggestionItem()
				{
					Value = "/damagedhousing/item/" + x.Id.ToString() + "/" + x.UrlNameUk,
					DisplayText = "[матеріал] - " + x.TitleUk,
					ValueText = "[матеріал] - " + x.TitleUk,
				}).ToList());

			model.AddRange(damagedHousingCategoryRepo.GetList(damagedHousingCategoryPredicate)
				.Select(x => new SuggestionItem()
				{
					Value = "/damagedhousing/category/" + x.Id.ToString() + "/" + x.UrlName,
					DisplayText = "[матеріал] - " + x.TitleUk,
					ValueText = "[матеріал] - " + x.TitleUk,
				}).ToList());

			model.AddRange(personRepo.GetList(personPredicate)
                .Select(x => new SuggestionItem()
                {
                    Value = "/persons/item/" + x.Id.ToString() + "/" + x.UrlNameUk,
                    DisplayText = "[персона] - " + x.TitleUk,
                    ValueText = "[персона] - " + x.TitleUk,
                }).ToList());

            model.AddRange(enterpriseRepo.GetList(enterprisePredicate)
                .Select(x => new SuggestionItem()
                {
                    Value = "/organizations/item/" + x.Id.ToString() + "/" + x.UrlNameUk,
                    DisplayText = "[організація] - " + x.TitleUk,
                    ValueText = "[організація] - " + x.TitleUk,
                }).ToList());

            model.AddRange(docRepo.GetList(docPredicate)
                .Select(x => new SuggestionItem()
                {
                    Value = "/documents/item/" + x.Id.ToString(),
                    DisplayText = "[документ] - " + x.TitleUk,
                    ValueText = "[документ] - " + x.TitleUk,
                }).ToList());

            model.AddRange(articleCategoryRepo.GetList(articleCategoryPredicate)
              .Select(x => new SuggestionItem()
              {
                  Value = "/articles/category/" + x.UrlName,
                  DisplayText = "[розділ матеріалів] - " + x.TitleUk,
                  ValueText = "[розділ матеріалів] - " + x.TitleUk,
              }).ToList());

            model.AddRange(personCategoryRepo.GetList(personCategoryPredicate)
              .Select(x => new SuggestionItem()
              {
                  Value = "/persons/category/" + x.UrlName,
                  DisplayText = "[розділ персон] - " + x.TitleUk,
                  ValueText = "[розділ персон] - " + x.TitleUk,
              }).ToList());

            model.AddRange(enterpriseCategoryRepo.GetList(enterpriseCategoryPredicate)
            .Select(x => new SuggestionItem()
            {
                Value = "/organizations/category/" + x.UrlName,
                DisplayText = "[розділ організацій] - " + x.TitleUk,
                ValueText = "[розділ організацій] - " + x.TitleUk,
            }).ToList());

            model.AddRange(docCategoryRepo.GetList(docCategoryPredicate)
            .Select(x => new SuggestionItem()
            {
                Value = "/documents/category/" + x.UrlName,
                DisplayText = "[розділ документів] - " + x.TitleUk,
                ValueText = "[розділ документів] - " + x.TitleUk,
            }).ToList());

            return model;
        }
    }
}
