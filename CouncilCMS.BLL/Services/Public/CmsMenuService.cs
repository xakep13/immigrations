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
    public class CmsMenuService : BaseService
    {
        private IRepository<Menu, int> menuRepo;
        private IRepository<Page, int> pageRepo;
        private IRepository<Article, int> articleRepo;
        private IRepository<DamagedHousing, int> damagedHousingRepo;
		private IRepository<Person, int> personRepo;
        private IRepository<Enterprise, int> enterpriseRepo;
        private IRepository<Doc, int> docRepo;
        private IRepository<ArticleCategory, int> articleCatRepo;
		private IRepository<DamagedHousingCategory, int> damagedHousingCatRepo;
		private IRepository<PersonCategory, int> personCatRepo;
        private IRepository<EnterpriseCategory, int> enterpriseCatRepo;
        private IRepository<DocCategory, int> docCatRepo;
        private IRepository<MenuItem, int> menuItemRepo;
        private IRepository<Session, int> sessionRepo;

        public CmsMenuService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public CmsMenuService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        { 
            menuRepo = UnitOfWork.GetIntRepository<Menu>();
            menuItemRepo = UnitOfWork.GetIntRepository<MenuItem>();
            pageRepo = UnitOfWork.GetIntRepository<Page>();
            articleRepo = UnitOfWork.GetIntRepository<Article>();
			damagedHousingRepo = UnitOfWork.GetIntRepository<DamagedHousing>();
			damagedHousingCatRepo = UnitOfWork.GetIntRepository<DamagedHousingCategory>();
			personRepo = UnitOfWork.GetIntRepository<Person>();
            enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
            docRepo = UnitOfWork.GetIntRepository<Doc>();
            articleCatRepo = UnitOfWork.GetIntRepository<ArticleCategory>();
            personCatRepo = UnitOfWork.GetIntRepository<PersonCategory>();
            enterpriseCatRepo = UnitOfWork.GetIntRepository<EnterpriseCategory>();
            docCatRepo = UnitOfWork.GetIntRepository<DocCategory>();
            sessionRepo = UnitOfWork.GetIntRepository<Session>();
        }

    
        public CmsMenu GetMenu(Int32 id, ShowMenuItemMode Mode, MenuItemType? currentType = null, MenuItemType? currentSecondType = null, String currentValue = null, String currentSecondValue = null)
        {
            var menu = menuRepo.GetById(id);
            var model = new CmsMenu();

            model.Id = menu.Id;
            model.Name = menu.GetLocalValue("Name");

            if (Mode == ShowMenuItemMode.AllLangualges)
            {
                model.Items = GetMenuItems(menu.MenuItems.Where(x => x.ParentMenuItemId == null && x.ShowItem).ToList(), Mode);
            }
            else
            {
                switch (CurrentCulture.Name.ToLower())
                {
                    case "uk":
                        model.Items = GetMenuItems(menu.MenuItems.Where(x => x.ParentMenuItemId == null && x.ShowItemUk).ToList(), Mode);
                        break;
                    case "ru":
                        model.Items = GetMenuItems(menu.MenuItems.Where(x => x.ParentMenuItemId == null && x.ShowItemRu).ToList(), Mode);
                        break;
                    case "en":
                        model.Items = GetMenuItems(menu.MenuItems.Where(x => x.ParentMenuItemId == null && x.ShowItemEn).ToList(), Mode);
                        break;
                }
            }

            if (currentType != null && !String.IsNullOrEmpty(currentValue))
            {
                SetCurrent(model.Items, currentType, currentSecondType, currentValue, currentSecondValue);
            }

            return model;
        }

        public CmsMenu GetMenu(String url, ShowMenuItemMode Mode, MenuItemType? currentType = null, MenuItemType? currentSecondType = null, String currentValue = null, String currentSecondValue = null)
        {
            var menu = menuRepo.GetSingle(x => x.Name == url);

            if (menu != null)
                return GetMenu(menu.Id, Mode, currentType, currentSecondType, currentValue, currentSecondValue);

            return null;
        }

        public List<CmsMenuItem> GetMenuItems(List<MenuItem> list, ShowMenuItemMode Mode)
        {
            var model = new List<CmsMenuItem>();

            if (list != null)
            {
                foreach (var item in list.OrderBy(x => x.Position))
                {
                    var menuItem = new CmsMenuItem()
                    {
                        Id = item.Id,
                        Title = item.GetLocalValue("Name"),
                        Image = item.Image,
                        HoverImage = item.HoverImage,
                        Position = item.Position,
                        Url = GetMenuItemUrl(item),
                        Type = (MenuItemType)item.Type,
                        Value = item.Value
                    };

                    if (Mode == ShowMenuItemMode.AllLangualges)
                    {
                        menuItem.Items = GetMenuItems(item.ChildMenuItems.Where(x => x.ShowItem).ToList(), Mode);
                    }
                    else
                    {
                        switch (CurrentCulture.Name.ToLower())
                        {
                            case "uk":
                                menuItem.Items = GetMenuItems(item.ChildMenuItems.Where(x => x.ShowItemUk).ToList(), Mode);
                                break;
                            case "ru":
                                menuItem.Items = GetMenuItems(item.ChildMenuItems.Where(x => x.ShowItemRu).ToList(), Mode);
                                break;
                            case "en":
                                menuItem.Items = GetMenuItems(item.ChildMenuItems.Where(x => x.ShowItemEn).ToList(), Mode);
                                break;
                        }

                    }

                    model.Add(menuItem);
                }
            }
            
            return model;
        }

        public Boolean SetCurrent(List<CmsMenuItem> source, MenuItemType? currentType = null, MenuItemType? currentSecondType = null, String currentValue = null, String currentSecondValue = null)
        {
            var setted = false;

            if (source != null)
            {
                foreach (var item in source)
                {
                    if (item.Current || item.Expanded)
                    {
                        item.Current = false;
                        item.Expanded = false;
                    }

                    if (item.Type == currentType && item.Value == currentValue)
                    {
                        item.Current = true;
                        item.Expanded = true;
                        setted = true;
                    }

                    var subSet = SetCurrent(item.Items, currentType, null, currentValue, null);

                    if (!setted)
                    {
                        setted = subSet;
                        if (setted)
                            item.Expanded = true;
                    }                    
                    
                }

                if (!setted && currentSecondValue != null && !String.IsNullOrEmpty(currentSecondValue))
                {
                    foreach (var item in source)
                    {
                        if (item.Current || item.Expanded)
                        {
                            item.Current = false;
                            item.Expanded = false;
                        }

                        if (item.Type == currentSecondType && item.Value == currentSecondValue)
                        {
                            item.Current = true;
                            item.Expanded = true;
                            setted = true;
                        }

                        var subSet = SetCurrent(item.Items, currentSecondType, null, currentSecondValue, null);

                        if (!setted)
                        {
                            setted = subSet;
                            if (setted)
                                item.Expanded = true;
                        }
                    }
                }

            }

            return setted;
        }

        public string GetMenuItemUrl(MenuItem item)
        {
            var result = item.Value;
            var id = 0;

            switch (item.Type)
            {
                case (int)MenuItemType.Page:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = pageRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlName }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/page/" + dest.url;
                    }

                    break;
                case (int)MenuItemType.Person:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = personRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlNameUk }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/persons/item/" + id + "/" + dest.url;
                    }

                    break;
                case (int)MenuItemType.Article:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = articleRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlNameUk }).FirstOrDefault();
                        
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/articles/item/" + id + "/" + dest.url;
                    }

					break;
				case (int)MenuItemType.DamagedHousing:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = damagedHousingRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlNameUk }).FirstOrDefault();

						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/damagedhousing/item/" + id + "/" + dest.url;
					}

					break;

				case (int)MenuItemType.DamagedHousingCategory:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = damagedHousingCatRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlName }).FirstOrDefault();

						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/damagedhousing/category/"  + dest.url + "/";
					}

					break;
				case (int)MenuItemType.Enterprise:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = enterpriseRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlNameUk }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/organizations/item/" + id + "/" + dest.url;
                    }

                    break;
                case (int)MenuItemType.Document:
                    if (Int32.TryParse(item.Value, out id))
                    {                                                
                        result = "/" + CurrentCulture.Name.ToLower() + "/documents/item/" + id + "/";
                    }

                    break;
                case (int)MenuItemType.ArticleCategory:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = articleCatRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlName }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/articles/category/" + dest.url + "/";
                    }

                    break;
                case (int)MenuItemType.PersonCategory:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = personCatRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlName }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/persons/category/" + dest.url + "/";
                    }

                    break;
                case (int)MenuItemType.EnterpriseCategory:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = enterpriseCatRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlName }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/organizations/category/" + dest.url + "/";
                    }

                    break;
                case (int)MenuItemType.DocumentCategory:
                    if (Int32.TryParse(item.Value, out id))
                    {
                        var dest = docCatRepo.GetList(x => x.Id == id).Select(x => new { url = x.UrlName }).FirstOrDefault();
                        if (dest != null)
                            result = "/" + CurrentCulture.Name.ToLower() + "/documents/category/" + dest.url + "/";
                    }
                    break;               
                case (int)MenuItemType.CouncilSession:
                    if (item.Value == "cur")
                    {
                        result = "/" + CurrentCulture.Name.ToLower() + "/sessions/current/projects/";
                    }
                    else
                    {
                        var now = DateTime.Now;
                        var session = sessionRepo.GetList(x => x.Published && x.PublishDate <= now && x.Ended, x => x.OrderByDescending(o => o.SessionDate)).Select(x => x.Id).DefaultIfEmpty(0).FirstOrDefault();

                        if (session > 0)
                        {
                            result = "/" + CurrentCulture.Name.ToLower() + "/sessions/" + session + "/resolutions/";
                        }
                        else
                        {
                            result = String.Empty;
                        }                       
                    }
                    break;

            }

            return result;
        }
    }
}
