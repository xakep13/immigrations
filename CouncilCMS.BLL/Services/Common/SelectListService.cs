using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.Core.Enums;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class SelectListService : BaseService
    {                
        private IRepository<ArticleCategory, int> articleCategoryRepo;
        private IRepository<PersonCategory, int> personCategoryRepo;
        private IRepository<EnterpriseCategory, int> enterpriseCategoryRepo;
        private IRepository<DocCategory, int> docCategoryRepo;
        private IRepository<ArticleCategoryTemplate, int> articleCategoryTemplateRepo;
        private IRepository<PersonCategoryTemplate, int> personCategoryTemplateRepo;
        private IRepository<EnterpriseCategoryTemplate, int> enterpriseCategoryTemplateRepo;
        private IRepository<EnterpriseType, int> enterpriseTypeRepo;
        private IRepository<DocCategoryTemplate, int> docCategoryTemplateRepo;
        private IRepository<DocType, int> docTypeRepo;     
        private IRepository<Menu, int> menuRepo;     
        private IRepository<ContentWidget, int> contentWidgetRepository;
        private IRepository<PageTemplate, int> pageTemplateRepository;
        private IRepository<CmsUser, int> cmsUserRepo;
        private IRepository<CmsRole, int> cmsRoleRepo;

        public SelectListService(string ConnectionString) : base(ConnectionString) { }
        public SelectListService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

              
        public List<SelectListItem> GetCmsUserSelectList(string notSelectedValue = null, string premissionPattern = null)
        {
            cmsUserRepo = cmsUserRepo ?? UnitOfWork.GetIntRepository<CmsUser>();
            var predicate = PredicateBuilder.True<CmsUser>();

            if (!String.IsNullOrEmpty(premissionPattern))
                predicate = predicate.And(x => x.Roles.Any(r => r.Premissions.Contains(premissionPattern)));

            predicate = predicate.And(x => !x.Blocked);

            var list = cmsUserRepo
                .GetList(predicate)
                .Select(x => new { Value = x.Id, TextRu = x.Name + " (" + x.Email + ")", TextUk = x.Name + " (" + x.Email + ")" }).ToList()
                .ToSelectList("Value", CurrentCulture.PropertyName("Text"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return list;
        }


        public List<SelectListItem> GetCmsRoleSelectList(string notSelectedValue = null, string premissionPattern = null)
        {
            cmsRoleRepo = cmsRoleRepo ?? UnitOfWork.GetIntRepository<CmsRole>();
            var predicate = PredicateBuilder.True<CmsRole>();

            if (!String.IsNullOrEmpty(premissionPattern))
                predicate = predicate.And(x => x.Premissions.Contains(premissionPattern));
            
            var list = cmsRoleRepo
                .GetList(predicate)
                .Select(x => new { Value = x.Id, TextRu = x.TitleRu, TextUk = x.TitleUk, TextEn = x.TitleEn }).ToList()
                .ToSelectList("Value", CurrentCulture.PropertyName("Text"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return list;
        }



        public List<SelectListItem> CategoryUrlSelectList<TCategory>(string notSelectedValue = null, int? userId = null) where TCategory : BaseCategoryEntity
        {
            var catRepo = UnitOfWork.GetIntRepository<TCategory>();

            var list = catRepo.GetList(null, x => x.OrderByColumn("Title" + CurrentCulture.ColumnLocalPrefix), "AllowedUsers,AllowedRoles").ToList();

            var selectList = new List<SelectListItem>();

            if (notSelectedValue != null)
                selectList.Add(new SelectListItem() { Value = notSelectedValue, Text = Locals.NotSelect });

            selectList = FillCategoryUrlSelectList(selectList, list.Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position).ToList(), list, 0, userId);

            return selectList;
        }
        public List<SelectListItem> CategorySelectList<TCategory>(string notSelectedValue = null, int? userId = null) where TCategory : BaseCategoryEntity
        {
            var catRepo = UnitOfWork.GetIntRepository<TCategory>();

            var list = catRepo.GetList(null, x => x.OrderByColumn("Title" + CurrentCulture.ColumnLocalPrefix), "AllowedUsers,AllowedRoles").ToList();

            var selectList = new List<SelectListItem>();

            if (notSelectedValue != null)
                selectList.Add(new SelectListItem() { Value = notSelectedValue, Text = Locals.NotSelect });

            selectList = FillCategorySelectList(selectList, list.Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position).ToList(), list, 0, userId);

            return selectList;
        }
        private List<SelectListItem> FillCategoryUrlSelectList<TCategory>(List<SelectListItem> targetList, List<TCategory> levelList, List<TCategory> sourceList, int level = 0, int? userId = null) where TCategory : BaseCategoryEntity
        {
            targetList = targetList ?? new List<SelectListItem>();
            var userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            var user = userRepo.GetById(userId ?? 0);

            foreach (var cat in levelList)
            {
                var listItem = new SelectListItem()
                {
                    Value = cat.UrlName,
                    Text = cat.GetLocalValue("Title"),
                    Disabled = user != null ? (!cat.AllowedUsers.Any(u => u.Id == userId) && !cat.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).Contains(r.Id))) : false
                };

                for (var i = 0; i < level; i++)
                {
                    listItem.Text = "-" + listItem.Text;
                }

                targetList.Add(listItem);

                if (sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
                {
                    FillCategoryUrlSelectList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1, userId);
                }
            }

            return targetList;
        }
        private List<SelectListItem> FillCategorySelectList<TCategory>(List<SelectListItem> targetList, List<TCategory> levelList, List<TCategory> sourceList, int level = 0, int? userId = null) where TCategory : BaseCategoryEntity
        {
            targetList = targetList ?? new List<SelectListItem>();
            var userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            var user = userRepo.GetById(userId ?? 0);

            foreach (var cat in levelList)
            {
                var listItem = new SelectListItem()
                {
                    Value = cat.Id.ToString(),
                    Text = cat.GetLocalValue("Title"),
                    Disabled = user != null ? (!cat.AllowedUsers.Any(u => u.Id == userId) && !cat.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).Contains(r.Id))) : false
                };

                for (var i = 0; i < level; i++)
                {
                    listItem.Text = "-" + listItem.Text;
                }

                targetList.Add(listItem);

                if (sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
                {
                    FillCategorySelectList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1, userId);
                }
            }

            return targetList;
        }

        public List<SelectListItem> CategoryTemplateSelectList<TTamplate>(string notSelectedValue = null) where TTamplate : BaseCategoryTemplate
        {
            var templateRepo = UnitOfWork.GetIntRepository<TTamplate>();

            var list = templateRepo.GetList(null, x => x.OrderByColumn("Title" + CurrentCulture.ColumnLocalPrefix)).ToList();

            var selectList = new List<SelectListItem>();

            selectList = list
                .ToSelectList("Id", CurrentCulture.PropertyName("Title"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return selectList;
        }

        public List<SelectListItem> GetEnterpriseTypeList(string notSelectedValue = null)
        {
            enterpriseTypeRepo = enterpriseTypeRepo ?? UnitOfWork.GetIntRepository<EnterpriseType>();

            var list = enterpriseTypeRepo.GetList(null, x => x.OrderBy(o => o.Position)).ToList();

            var selectList = new List<SelectListItem>();

            if (notSelectedValue != null)
                selectList.Add(new SelectListItem() { Value = notSelectedValue, Text = Locals.NotSelect });

            selectList = list
                .ToSelectList("Id", CurrentCulture.PropertyName("Title"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return selectList;
        }
        public List<SelectListItem> GetDocTypeSelectList(string notSelectedValue = null)
        {
            docTypeRepo = docTypeRepo ?? UnitOfWork.GetIntRepository<DocType>();

            var list = docTypeRepo.GetList(null, x => x.OrderByColumn("Title" + CurrentCulture.ColumnLocalPrefix)).ToList();

            var selectList = new List<SelectListItem>();

            selectList = list
                .ToSelectList("Id", CurrentCulture.PropertyName("Title"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return selectList;
        }


        public List<SelectListItem> GetMenuSelectList(string notSelectedValue = null)
        {
            menuRepo = menuRepo ?? UnitOfWork.GetIntRepository<Menu>();

            var list = menuRepo
                .GetList(null, x => x.OrderByColumn("Name" + CurrentCulture.ColumnLocalPrefix))
                .Select(x => new { Value = x.Id, TextRu = x.NameRu, TextUk = x.NameUk }).ToList()
                .ToSelectList("Value", CurrentCulture.PropertyName("Text"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return list;
        }

        public List<SelectListItem> GetPageTemplateSelectList(string notSelectedValue = null)
        {
            pageTemplateRepository = pageTemplateRepository ?? UnitOfWork.GetIntRepository<PageTemplate>();

            var list = pageTemplateRepository
                .GetList(null, x => x.OrderBy(o => o.Position))
                .Select(x => new { Value = x.Id, TextRu = x.TitleRu, TextUk = x.TitleUk }).ToList()
                .ToSelectList("Value", CurrentCulture.PropertyName("Text"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return list;
        }

        public List<SelectListItem> GetContentWidgetSelectList(string notSelectedValue = null)
        {
            contentWidgetRepository = contentWidgetRepository ?? UnitOfWork.GetIntRepository<ContentWidget>();

            var list = contentWidgetRepository
                .GetList(null, x => x.OrderBy(o => o.Position))
                .Select(x => new { Value = x.Name, TextRu = x.TitleRu, TextUk = x.TitleUk }).ToList()
                .ToSelectList("Value", CurrentCulture.PropertyName("Text"), notSelectedValue, Locals.NotSelect) ??
                new List<SelectListItem>();

            return list;
        }
    }
}
