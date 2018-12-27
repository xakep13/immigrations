using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.BLL.ViewModels;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;


namespace Bissoft.CouncilCMS.BLL.Services
{
    public class CmsPageService : BaseService
    {
        private IRepository<Page, int> pageRepo;
        private CmsContentService contentService;
        private CmsMenuService menuService;
        public CmsPageService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsPageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {            
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            contentService = new CmsContentService(this.UnitOfWork);
            menuService = new CmsMenuService(this.UnitOfWork);
        }

      
        public CmsPage GetPage(String url, ShowMenuItemMode Mode = ShowMenuItemMode.AllLangualges, int? userId = null)
        {
            var date = DateTime.Now;
            var item = pageRepo.GetSingle(x => x.UrlName == url && x.Published && x.PublishDate <= date, "ContentRows", true, true);

            if (item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
                return null;
            
            var model = new CmsPage() { ContentRows = new List<CmsContentRow>() };

            model.Id = item.Id;
            model.Title = item.GetLocalValue("Title");
            model.Description = item.GetLocalValue("Description");
            model.UrlName = item.UrlName;
            model.PublishDate = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.PublishDate) : String.Empty;
            model.LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.EditedDate) : String.Empty;
            model.MetaDescription = item.GetLocalValue("MetaDescription");
            model.MetaKeywords = item.GetLocalValue("MetaKeywords");
            model.MetaTitle = item.GetLocalValue("MetaTitle");
            model.PageTemplateId = item.PageTemplateId;
            model.Type = item.Type;
            model.ViewCount = item.ViewCount;
            model.ContentRows = contentService.GetContentRows(item.ContentRows);
            model.PageMenu = item.SidebarMenuId.HasValue ? menuService.GetMenu(item.SidebarMenuId.Value, Mode) : null;

            AddView(item.Id);

            return model;          
        }
        
        public void AddView(Int32 id)
        {
            pageRepo.Context.ExecuteSqlCommand("UPDATE Pages SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
        }
    }
}
