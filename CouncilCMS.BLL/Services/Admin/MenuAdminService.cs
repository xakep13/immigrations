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
using System.Web.Mvc;
using System.Linq.Expressions;

namespace Bissoft.CouncilCMS.BLL.Services
{
	public class MenuAdminService : BaseService
	{
		private IRepository<Menu, int> menuRepo;
		private IRepository<MenuItem, int> menuItemRepo;
		private IRepository<Page, int> pageRepo;
		private IRepository<Article, int> articleRepo;
		private IRepository<Person, int> personRepo;
		private IRepository<Enterprise, int> enterpriseRepo;
		private IRepository<Doc, int> docRepo;
		private IRepository<ArticleCategory, int> articleCatRepo;
		private IRepository<PersonCategory, int> personCatRepo;
		private IRepository<EnterpriseCategory, int> enterpriseCatRepo;
		private IRepository<DocCategory, int> docCatRepo;
		private IRepository<CmsUser, int> userRepo;
		private IRepository<CmsRole, int> roleRepo;
		private SelectListService selectListService;
		public MenuAdminService(string ConnectionString) : base(ConnectionString)
		{
			Initialize();
		}

		public MenuAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			Initialize();
		}

		private void Initialize()
		{
			menuRepo = UnitOfWork.GetIntRepository<Menu>();
			menuItemRepo = UnitOfWork.GetIntRepository<MenuItem>();
			pageRepo = UnitOfWork.GetIntRepository<Page>();
			articleRepo = UnitOfWork.GetIntRepository<Article>();
			personRepo = UnitOfWork.GetIntRepository<Person>();
			enterpriseRepo = UnitOfWork.GetIntRepository<Enterprise>();
			docRepo = UnitOfWork.GetIntRepository<Doc>();
			articleCatRepo = UnitOfWork.GetIntRepository<ArticleCategory>();
			personCatRepo = UnitOfWork.GetIntRepository<PersonCategory>();
			enterpriseCatRepo = UnitOfWork.GetIntRepository<EnterpriseCategory>();
			docCatRepo = UnitOfWork.GetIntRepository<DocCategory>();
			userRepo = UnitOfWork.GetIntRepository<CmsUser>();
			roleRepo = UnitOfWork.GetIntRepository<CmsRole>();
			selectListService = new SelectListService(this.UnitOfWork);
		}


		public List<MenuListItem> GetListItems(int? userId = null)
		{
			var list = menuRepo.GetList(notDeleted: false).ToList();
			var menuItems = menuItemRepo.GetList(notDeleted: false).ToList();
			var user = userId > 0 ? userRepo.GetById(userId.Value) : null;
			var model = new List<MenuListItem>();

			foreach(var menu in list)
			{
				model.Add(new MenuListItem()
				{
					Id = menu.Id,
					NameRu = menu.NameRu,
					NameUk = menu.NameUk,
					NameEn = menu.NameEn,
					Index = menu.Index,
					MenuItems = menuItems
						.Where(x => x.MenuId == menu.Id && x.ParentMenuItemId == null)
						.OrderBy(x => x.Position)
						.Select(x => FillMenuItem(x, menuItems, user))
						.ToList()
				});
			}

			return model;
		}
		/*
        public MenuListItem GetItem(int itemId, int? userId = null)
        {

            var item = 
            new MenuListItem()
            {
                Id = menu.Id,
                NameRu = menu.NameRu,
                NameUk = menu.NameUk,
                NameEn = menu.NameEn,
                MenuItems = menuItems
                        .Where(x => x.MenuId == menu.Id && x.ParentMenuItemId == null)
                        .OrderBy(x => x.Position)
                        .Select(x => FillMenuItem(x, menuItems, user))
                        .ToList()
            };

        }
        */
		public MenuEdit GetMenuEdit(Int32 Id)
		{
			var model = new MenuEdit();

			var item = menuRepo.GetById(Id) ?? new Menu() { Id = 0 };

			model.Id = item.Id;
			model.Name = item.Name;
			model.NameRu = item.NameRu;
			model.NameUk = item.NameUk;
			model.NameEn = item.NameEn;

			return model;
		}

		public void UpdateSubMenuPosition(int? menuId, int parentId, int[] menuIds)
		{
			Expression<Func<MenuItem, bool>> filter;
			if(menuId != null)
			{
				filter = (x) => x.MenuId == menuId && x.ParentMenuItemId == parentId;
			}
			else
			{
				filter = (x) => x.MenuId == parentId;
			}

			List<MenuItem> menuItems = menuItemRepo.GetList(
				filter: filter,
				asNoTracking: false).ToList();

			for(int i = 0; i < menuIds.Length; i++)
			{
				var menuItem = menuItems.First(mi => mi.Id == menuIds[i]);
				menuItem.Position = i;

			}

			menuItemRepo.Context.SaveChanges();

			// get all sub menus 
			/*
            Menu parentMenu = menuRepo.GetById(parentId);
            var subMenus = parentMenu.MenuItems.ToList();

            List<MenuItem> newList = new List<MenuItem>();
            int j = 0;
            foreach (var i in menuIds)
            {
                MenuItem elem = subMenus.FirstOrDefault(y => y.Id == i);
                elem.Position = j;
                if (elem != null)
                {
                    newList.Add(elem);
                }
                j++;
            }

            parentMenu.MenuItems = newList;

            menuRepo.Context.SaveChanges();
            */
			/*
            int j = 0;
            foreach (var sm in subMenus)
            {
                sm.Position = j;
                j++;
            }
            menuRepo.Context.SaveChanges();
            */
		}


		public void UpdateSubChildrenMenuPosition(int parentId, int[] menuIds)
		{
			// get all sub menus 
			MenuItem parentMenu = menuItemRepo.GetById(parentId);

			List<MenuItem> children; //= new List<MenuItem>();
			children = menuItemRepo.GetList(x => menuIds.Contains(x.Id)).ToList();

			for(int i = 0; i < menuIds.Length; i++)
			{
				var menuItem = children.First(mi => mi.Id == menuIds[i]);
				menuItem.Position = i;
			}
			;
			//menuRepo.Context.SaveChanges();

		}

		public MenuItemEdit GetMenuItemEdit(Int32 id, Int32 menuId, Int32? parentId = null, int? userId = null)
		{
			var model = new MenuItemEdit();
			var item = menuItemRepo.GetById(id) ?? new MenuItem() { Id = 0, MenuId = menuId, ParentMenuItemId = parentId };

			var pagess = pageRepo.GetList();

			List<SelectListItem> pages = new List<SelectListItem>();

			foreach(var p in pagess)
			{
				pages.Add(new SelectListItem
				{
					Text = p.TitleUk,
					Value = "/uk/page/" + p.UrlName,
					
				});
			}

			model.Id = item.Id;
			model.Pages = pages;
			model.MenuId = item.MenuId;
			model.ParentMenuId = item.ParentMenuItemId;
			model.Position = item.Position;
			model.NameRu = item.NameRu;
			model.NameUk = item.NameUk;
			model.NameEn = item.NameEn;
			model.DescriptionRu = item.DescriptionRu;
			model.DescriptionUk = item.DescriptionUk;
			model.DescriptionEn = item.DescriptionEn;
			model.Image = item.Image;
			model.HoverImage = item.HoverImage;
			model.Type = (MenuItemType)item.Type;
			model.Value = item.Value;
			model.ValueText = GetMenuItemVelueText(item);
			model.ShowItem = item.ShowItem;
			model.ShowItemUk = item.ShowItemUk;
			model.ShowItemRu = item.ShowItemRu;
			model.ShowItemEn = item.ShowItemEn;
			model.Types = ((MenuItemType)item.Type).ToSelectList();
			model.PersonCategories = selectListService.CategorySelectList<PersonCategory>(userId: userId);
			model.ArticleCategories = selectListService.CategorySelectList<ArticleCategory>(userId: userId);
			model.DocCategories = selectListService.CategorySelectList<DocCategory>(userId: userId);
			model.EnterpriseCategories = selectListService.CategorySelectList<EnterpriseCategory>(userId: userId);
			model.Roles = selectListService.GetCmsRoleSelectList();
			model.AllowedRoles = item.AllowedRoles != null ? item.AllowedRoles.Select(x => new AllowedRole() { RoleId = x.Id, ItemId = item.Id, Name = x.TitleUk }).ToList() : null;

			return model;
		}

		public MenuListItem SaveMenu(MenuEdit model)
		{
			var item = menuRepo.GetById(model.Id) ?? new Menu() { Id = 0 };

			model.NameUk = String.IsNullOrEmpty(model.NameUk) ? "Без назви" : model.NameUk;
			model.NameRu = String.IsNullOrEmpty(model.NameRu) ? "Без названия" : model.NameRu;
			model.NameEn = String.IsNullOrEmpty(model.NameEn) ? "No name" : model.NameEn;

			item.Name = model.Name;
			item.NameRu = model.NameRu;
			item.NameUk = model.NameUk;
			item.NameEn = model.NameEn;

			menuRepo.InsertOrUpdate(item);
			UnitOfWork.Commit();

			var result = new MenuListItem()
			{
				Id = item.Id,
				NameRu = item.NameRu,
				NameUk = item.NameUk,
				NameEn = item.NameEn
			};

			return result;
		}

		public MenuListItem CreateMenu(int menuItemId)
		{
			var item = menuItemRepo.GetById(menuItemId);

			if(item != null)
			{
				var menu = new Menu() { Id = -1, NameUk = item.NameUk, NameRu = item.NameRu, NameEn = item.NameEn, Name = item.Url };

				menuRepo.Insert(menu);
				UnitOfWork.Commit();

				var items = item.ChildMenuItems;
			}

			return null;
		}

		public MenuItemListItem SaveMenuItem(MenuItemEdit model)
		{
			var item = menuItemRepo.GetById(model.Id) ?? new MenuItem() { Id = 0 };

			model.NameUk = String.IsNullOrEmpty(model.NameUk) ? "Без назви" : model.NameUk;
			model.NameRu = String.IsNullOrEmpty(model.NameRu) ? "Без названия" : model.NameRu;
			model.NameEn = String.IsNullOrEmpty(model.NameEn) ? "No name" : model.NameEn;

			item.NameRu = model.NameRu;
			item.NameUk = model.NameUk;
			item.NameEn = model.NameEn;
			item.DescriptionUk = model.DescriptionUk;
			item.DescriptionRu = model.DescriptionRu;
			item.DescriptionEn = model.DescriptionEn;
			item.Image = model.Image;
			item.HoverImage = model.HoverImage;
			item.Type = (int)model.Type;
			item.Value = model.Value;
			item.ShowItem = model.ShowItem;
			item.ShowItemUk = model.ShowItemUk;
			item.ShowItemRu = model.ShowItemRu;
			item.ShowItemEn = model.ShowItemEn;
			item.Position = model.Id > 0 ? item.Position : (GetMaxPosition(model.ParentMenuId, model.MenuId) + 1);
			item.ParentMenuItemId = model.ParentMenuId;
			item.MenuId = model.MenuId;

			menuItemRepo.InsertOrUpdate(item);
			UnitOfWork.Commit();

			var result = new MenuItemListItem()
			{
				TitleRu = item.NameRu,
				TitleUk = item.NameUk,
				TitleEn = item.NameEn,
				Image = item.Image,
				Position = item.Position,
				MenuId = item.MenuId,
				Type = ((MenuItemType)item.Type).GetLocalDescription(),
				Id = item.Id,
				Url = GetMenuItemUrl(item),
				ShowItem = item.ShowItem,
				ShowItemUk = item.ShowItemUk,
				ShowItemEn = item.ShowItemEn,
				ShowItemRu = item.ShowItemRu,
				ParentMenuId = item.ParentMenuItemId,
				Allowed = true
			};

			result.HasChilds = menuItemRepo.DbSet.Any(x => x.ParentMenuItemId == item.Id);

			return result;
		}

		public void UpdatePosition(Int32 id, int? parentId, int menuId, Int32 oldPosition, Int32 newPosition)
		{
			var result = 0;

			if(oldPosition > newPosition)
			{
				result = menuItemRepo
					.Context
					.ExecuteSqlCommand("UPDATE MenuItems SET Position = Position + 1 WHERE Position >= {0} AND Position < {1} AND MenuId = {2} AND ParentMenuItemId " + (parentId.HasValue ? (" = " + parentId) : "is null"), false, null, newPosition, oldPosition, menuId);
			}
			else if(oldPosition < newPosition)
			{
				result = menuItemRepo
				   .Context
				   .ExecuteSqlCommand("UPDATE MenuItems SET Position = Position - 1 WHERE Position <= {0} AND Position > {1} AND MenuId = {2} AND ParentMenuItemId " + (parentId.HasValue ? (" = " + parentId) : "is null"), false, null, newPosition, oldPosition, menuId);
			}

			result = menuItemRepo
				   .Context
				   .ExecuteSqlCommand("UPDATE MenuItems SET Position = {0} WHERE Id = {1}", false, null, newPosition, id);
		}

		public void Delete(Int32 Id)
		{
			var menu = menuRepo.GetById(Id);

			menu.Deleted = true;
			menuRepo.Update(menu);

			UnitOfWork.Commit();
		}

		public void Remove(Int32 Id)
		{
			var menu = menuRepo.GetById(Id);

			menuRepo.Delete(menu);

			UnitOfWork.Commit();
		}


		public void DeleteMenuItem(Int32 Id)
		{
			var menuItem = menuItemRepo.GetById(Id);

			menuItem.Deleted = true;
			menuItemRepo.Update(menuItem);

			UnitOfWork.Commit();
		}

		public void RemoveMenuItem(Int32 Id)
		{
			var menuItem = menuItemRepo.GetById(Id);

			menuItemRepo.Delete(menuItem);

			UnitOfWork.Commit();

			menuItemRepo
				   .Context
				   .ExecuteSqlCommand("UPDATE MenuItems SET Position = Position - 1 WHERE Position > {0} AND MenuId = {1} AND ParentMenuItemId " + (menuItem.ParentMenuItemId.HasValue ? (" = " + menuItem.ParentMenuItemId) : "is null"), false, null, menuItem.Position, menuItem.MenuId);
		}

		private Int32 GetMaxPosition(Int32? MenuItemParentId, Int32 MenuId)
		{
			var max = menuItemRepo.GetList().Where(x => x.MenuId == MenuId && x.ParentMenuItemId == MenuItemParentId).Select(x => x.Position).DefaultIfEmpty(0).Max();

			return max;
		}

		private MenuItemListItem FillMenuItem(MenuItem item, List<MenuItem> list, CmsUser user = null)
		{
			var model = new MenuItemListItem()
			{
				TitleRu = item.NameRu,
				TitleUk = item.NameUk,
				TitleEn = item.NameEn,
				Image = item.Image,
				Position = item.Position,
				MenuId = item.MenuId,
				Id = item.Id,
				Type = ((MenuItemType)item.Type).GetLocalDescription(),
				Url = GetMenuItemUrl(item),
				ShowItem = item.ShowItem,
				ShowItemUk = item.ShowItemUk,
				ShowItemRu = item.ShowItemRu,
				ShowItemEn = item.ShowItemEn,
				ParentMenuId = item.ParentMenuItemId,
				Allowed = user != null ? (item.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).DefaultIfEmpty(0).Contains(r.Id))) : true,
				HasChilds = list.Count(x => x.ParentMenuItemId == item.Id) > 0,
				ChildMenuItems = list.Count(x => x.ParentMenuItemId == item.Id) > 0 ?
					list.Where(x => x.ParentMenuItemId == item.Id).OrderBy(x => x.Position).Select(x => FillMenuItem(x, list, user)).ToList() :
					null
			};

			return model;
		}

		public string GetMenuItemUrl(MenuItem item)
		{
			var result = item.Value;
			var id = 0;

			switch(item.Type)
			{
				case (int)MenuItemType.Page:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = pageRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/page/" + dest.UrlName;
					}

					break;
				case (int)MenuItemType.Person:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = personRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/persons/item/" + id + "/" + dest.UrlNameUk;
					}

					break;
				case (int)MenuItemType.Article:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = articleRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/articles/item/" + id + "/" + dest.UrlNameUk;
					}

					break;
				case (int)MenuItemType.Enterprise:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = enterpriseRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/organizations/item/" + id + "/" + dest.UrlNameUk;
					}

					break;
				case (int)MenuItemType.Document:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = docRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/documents/item/" + id + "/";
					}

					break;
				case (int)MenuItemType.ArticleCategory:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = articleCatRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/articles/category/" + dest.UrlName + "/";
					}

					break;
				case (int)MenuItemType.PersonCategory:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = personCatRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/persons/category/" + dest.UrlName + "/";
					}

					break;
				case (int)MenuItemType.EnterpriseCategory:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = enterpriseCatRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/organizations/category/" + dest.UrlName + "/";
					}

					break;
				case (int)MenuItemType.DocumentCategory:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = docCatRepo.GetById(id);
						if(dest != null)
							result = "/" + CurrentCulture.Name.ToLower() + "/documents/category/" + dest.UrlName + "/";
					}

					break;
			}

			return result;
		}

		private String GetMenuItemVelueText(MenuItem item)
		{
			var result = string.Empty;
			var id = 0;

			switch(item.Type)
			{
				case (int)MenuItemType.Page:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = pageRepo.GetById(id);
						if(dest != null)
							result = dest.GetLocalValue("Title") + " [id: " + id + "]";
					}

					break;
				case (int)MenuItemType.Person:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = personRepo.GetById(id);
						if(dest != null)
							result = dest.GetLocalValue("Title") + " [id: " + id + "]";
					}

					break;
				case (int)MenuItemType.Article:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = articleRepo.GetById(id);
						if(dest != null)
							result = dest.GetLocalValue("Title") + " [id: " + id + "]";
					}

					break;
				case (int)MenuItemType.Enterprise:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = enterpriseRepo.GetById(id);
						if(dest != null)
							result = dest.GetLocalValue("Title") + " [id: " + id + "]";
					}

					break;
				case (int)MenuItemType.Document:
					if(Int32.TryParse(item.Value, out id))
					{
						var dest = docRepo.GetById(id);
						if(dest != null)
							result = dest.GetLocalValue("Title") + " [id: " + id + "]";
					}

					break;

			}

			return result;
		}

		public AllowedRole AddRole(AllowedRole model)
		{
			var item = menuItemRepo.GetById(model.ItemId);
			var role = roleRepo.GetById(model.RoleId);

			if(item.AllowedRoles == null)
				item.AllowedRoles = new List<CmsRole>();

			if(role != null)
			{
				var allowedRole = item.AllowedRoles.FirstOrDefault(x => x.Id == model.RoleId);

				if(allowedRole == null)
				{
					item.AllowedRoles.Add(role);

					menuItemRepo.Update(item);
					UnitOfWork.Commit();

					model.Name = role.GetLocalValue("Title");

					return model;
				}
			}

			return null;
		}

		public void DeleteRole(Int32 ItemId, Int32 RoleId)
		{
			var item = menuItemRepo.GetById(ItemId);
			var role = item.AllowedRoles.FirstOrDefault(x => x.Id == ItemId);

			if(role != null)
			{
				item.AllowedRoles.Remove(role);
			}

			menuItemRepo.Update(item);
			UnitOfWork.Commit();
		}
	}
}
