using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.Entities.Entities;
using Bissoft.CouncilCMS.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using xTab.Tools.Extensions;
using xTab.Tools.Helpers;

namespace Bissoft.CouncilCMS.BLL.Services
{
	public class DamagedHousingAdminService : BaseService
	{
		private IRepository<DamagedHousing, int> damagedHousingRepo;
		private IRepository<DamagedHousingCategory, int> categoryRepo;
		private IRepository<CmsUser, int> userRepo;
		private ContentAdminService contentAdminService;
		private SelectListService selectListService;
		private CmsSearchAdminService cmsSearchService;

		public DamagedHousingAdminService(string ConnectionString) : base(ConnectionString)
		{
			Initialize();
		}

		public DamagedHousingAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			Initialize();
		}

		private void Initialize()
		{
			damagedHousingRepo = UnitOfWork.GetRepository<DamagedHousing, int>();
			categoryRepo = UnitOfWork.GetRepository<DamagedHousingCategory, int>();
			userRepo = UnitOfWork.GetIntRepository<CmsUser>();
			contentAdminService = new ContentAdminService(this.UnitOfWork);
			selectListService = new SelectListService(this.UnitOfWork);
			cmsSearchService = new CmsSearchAdminService(this.UnitOfWork);
		}

		public List<DamagedHousingCategory> CategoriesForAdmin()
		{
			var categories = categoryRepo.GetList(includeProperties: "AllowedUsers,AllowedRoles").ToList();
			return categories;
		}

		public DamagedHousingList List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, Int32 page = 1, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
		{
			var model = new DamagedHousingList()
			{
				Page = page,
				Query = query,
				CategoryId = categoryId,
				DateRange = dateRange,
				DateType = dateType,
				ItemState = (int)itemState,
				SortBy = sortBy,
				SortDirection = sortDir,
				Categories = selectListService.CategorySelectList<DamagedHousingCategory>(String.Empty, userId),
				User = user,
				UserAction = userAction,
				Users = selectListService.GetCmsUserSelectList(String.Empty)
			};

			return model;
		}

		public List<DamagedHousingListItem> GetList(out int count, string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, CmsItemState itemState = CmsItemState.All, int page = 1, Int32 perPage = 20, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
		{
			var predicate = CreateStatePredicate<DamagedHousing>(itemState);

			if(!String.IsNullOrEmpty(query))
				predicate = TitleFilter(predicate, query);

			if(!String.IsNullOrEmpty(dateRange))
				predicate = DateRangeFilter(predicate, dateRange, dateType);

			if(user > 0)
				predicate = UserFilter(predicate, user, userAction);

			if(categoryId > 0)
				predicate = predicate.And(x => x.Categories.Any(c => c.Id == categoryId));

			if(userId > 0)
			{
				var roles = userRepo.GetById(userId.Value, "Roles", true).Roles.Select(x => x.Id);
				predicate = predicate.And(x => x.Categories.Any(c => c.AllowedUsers.Any(u => u.Id == userId)) || x.Categories.Any(c => c.AllowedRoles.Any(r => roles.Contains(r.Id))));
			}

			var list = damagedHousingRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "Categories,CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false).ToList();

			var model = list.Select(x => new DamagedHousingListItem()
			{
				Id = x.Id,

				

				TitleRu = x.TitleRu,
				TitleUk = x.TitleUk,
				TitleEn = x.TitleEn,

				Deleted = x.Deleted,
				CreateDate = x.CreateDate,
				LastEditDate = x.LastEditDate,
				PublishDate = x.PublishDate,
				Published = x.Published,
				EditedDate = x.EditedDate,
				ViewCount = x.ViewCount,

				CategoriesUk = x.Categories.Select(c => c.TitleUk),
				CategoriesRu = x.Categories.Select(c => c.TitleRu),
				CategoriesEn = x.Categories.Select(c => c.TitleEn),

				CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
				LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
			}).ToList();

			return model;
		}

		public DamagedHousingEdit GetForEdit(Int32 id, int? userId = null)
		{
			var article = damagedHousingRepo.GetById(id);
			var categories = categoryRepo.GetList(includeProperties: "AllowedUsers,AllowedRoles").ToList();

			if(id == 0)
			{
				article = new DamagedHousing()
				{
					Id = 0,
					TitleUk = "Без назви",
					TitleRu = "Без названия",
					TitleEn = "No name",
					Categories = new List<DamagedHousingCategory>(),
					ContentRows = new List<ContentRow>(),
					CreateDate = DateTime.Now,
					PublishDate = DateTime.Now,
					ShowPublihDate = true,
					ShowEditDate = false,
					Lng = 0,
					Lat = 0,
					Year = DateTime.Now.Year,
					Published = true,
					CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
				};

				damagedHousingRepo.Insert(article);
				UnitOfWork.Commit();

				article.TitleUk = null;
				article.TitleRu = null;
				article.TitleEn = null;
			}

			var model = new DamagedHousingEdit
			{
				Email = article.Email,
				Adress = article.Adress,
				FullName = article.FullName,
				StartWork = DateTimeHelper.NullableDateTimeString(article.StartWork, null, false, "dd.MM.yyyy HH:mm"),
				EndWork = DateTimeHelper.NullableDateTimeString(article.EndWork, null, false, "dd.MM.yyyy HH:mm"),
				Year = article.Year,
				Price = article.Price,
				Lat = article.Lat,
				Lng = article.Lng,
				FinanceSource = article.FinanceSource,
				FinanceType = article.FinanceType,
				Status = article.Status,
				
				Id = article.Id,
				TitleRu = article.TitleRu,
				TitleUk = article.TitleUk,
				TitleEn = article.TitleEn,
				DescriptionRu = article.DescriptionRu,
				DescriptionUk = article.DescriptionUk,
				DescriptionEn = article.DescriptionEn,
				MetaTitleRu = article.MetaTitleRu,
				MetaTitleUk = article.MetaTitleUk,
				MetaTitleEn = article.MetaTitleEn,
				MetaKeywordsRu = article.MetaKeywordsRu,
				MetaKeywordsUk = article.MetaKeywordsUk,
				MetaKeywordsEn = article.MetaKeywordsEn,
				MetaDescriptionRu = article.MetaDescriptionRu,
				MetaDescriptionUk = article.MetaDescriptionUk,
				MetaDescriptionEn = article.MetaDescriptionEn,
				Image = article.Image,
				AllowComments = article.AllowComments,
				Published = article.Published,
				ShowEditDate = article.ShowEditDate,
				ShowPublihDate = article.ShowPublihDate,

				PublishDate = DateTimeHelper.NullableDateTimeString(article.PublishDate, null, false, "dd.MM.yyyy HH:mm"),
				EventDate = DateTimeHelper.NullableDateTimeString(article.EventDate, null, false, "dd.MM.yyyy HH:mm"),
				EditedDate = null
			};

			model.DamagedHousingCategories =
				GetCategoryCheckedList(
					model.DamagedHousingCategories,
					categories.Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position).ToList(),
					categories,
					0, userId);

			foreach(var item in article.Categories.Select(x => x.Id).ToList())
			{
				model.DamagedHousingCategories.Where(x => x.Value == item).FirstOrDefault().IsChecked = true;
			}

			model.ContentRows = contentAdminService.GetContentRowListItems(article.ContentRows, model.Id);

			return model;
		}

		public DamagedHousingEdit Save(DamagedHousingEdit model)
		{
			var item = damagedHousingRepo.GetById(model.Id);

			model.TitleUk = String.IsNullOrEmpty(model.TitleUk) ? "Без назви" : model.TitleUk;
			model.TitleRu = String.IsNullOrEmpty(model.TitleRu) ? "Без названия" : model.TitleRu;
			model.TitleEn = String.IsNullOrEmpty(model.TitleEn) ? "No name" : model.TitleEn;

			var texts = contentAdminService.GetContentText(item.ContentRows);
			var textRu = model.Adress + " " + model.DescriptionRu + " " + texts[0];
			var textUk = model.Adress + " " + model.DescriptionUk + " " + texts[1];
			var textEn = model.Adress + " " + model.DescriptionEn + " " + texts[2];

			item.Id = model.Id;

			item.TitleRu = model.TitleRu;
			item.TitleUk = model.TitleUk;
			item.TitleEn = model.TitleEn;

			item.DescriptionRu = model.DescriptionRu;
			item.DescriptionUk = model.DescriptionUk;
			item.DescriptionEn = model.DescriptionEn;

			item.UrlNameRu = model.TitleRu.Translit();
			item.UrlNameUk = model.TitleUk.Translit();
			item.UrlNameEn = model.TitleEn.Translit();

			item.MetaTitleRu = model.MetaTitleRu;
			item.MetaTitleUk = model.MetaTitleUk;
			item.MetaTitleEn = model.MetaTitleEn;

			item.MetaKeywordsRu = model.MetaKeywordsRu;
			item.MetaKeywordsUk = model.MetaKeywordsUk;
			item.MetaKeywordsEn = model.MetaKeywordsEn;

			item.MetaDescriptionRu = model.MetaDescriptionRu;
			item.MetaDescriptionUk = model.MetaDescriptionUk;
			item.MetaDescriptionEn = model.MetaDescriptionEn;

			item.ShowPublihDate = model.ShowPublihDate;
			item.AllowComments = model.AllowComments;
			item.ShowEditDate = model.ShowEditDate;

			item.FullName = model.FullName;
			item.Adress = model.Adress;
			item.Email = model.Email;
			item.StartWork = DateTimeHelper.ParseDateNullable(model.StartWork, null, DateTimeHelper.DefaultTimeOfDay.None);
			item.EndWork = DateTimeHelper.ParseDateNullable(model.EndWork, null, DateTimeHelper.DefaultTimeOfDay.None);
			item.Status = model.Status;
			item.FinanceSource = model.FinanceSource;
			item.FinanceType = model.FinanceType;
			item.Lat = model.Lat;
			item.Lng = model.Lng;
			item.Year = model.Year;
			item.Price = model.Price;

			item.Image = model.Image;
			item.Published = model.Published;
			item.LastEditDate = DateTime.Now;
			item.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
			item.EventDate = DateTimeHelper.ParseDateNullable(model.EventDate, null, DateTimeHelper.DefaultTimeOfDay.None);
			item.EditedDate = DateTimeHelper.ParseDateNullable(model.EditedDate, DateTime.Now, DateTimeHelper.DefaultTimeOfDay.None);
			item.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;

			if(item.Categories != null)
				item.Categories.Clear();
			else
				item.Categories = new List<DamagedHousingCategory>();

			damagedHousingRepo.Update(item);

			if(model.DamagedHousingCategories!=null)
				foreach(var cat in model.DamagedHousingCategories)
				{
					if(cat.IsChecked)
						item.Categories.Add(categoryRepo.GetById(cat.Value));
				}

			if(item.Id > 0)
			{
				damagedHousingRepo.Update(item);
			}
			else
			{
				damagedHousingRepo.Insert(item);
			}

			UnitOfWork.Commit();

			cmsSearchService.Save(item.Id, textUk, textRu, textEn, item.PublishDate, CmsSearchType.Article, item.Published);

			return model;
		}

		public void Delete(Int32 Id)
		{
			var article = damagedHousingRepo.GetById(Id);

			article.Deleted = true;
			damagedHousingRepo.Update(article);
			cmsSearchService.Delete(Id, CmsSearchType.Article);

			UnitOfWork.Commit();
		}

		public void Restore(Int32 Id)
		{
			var article = damagedHousingRepo.GetById(Id);

			article.Deleted = false;
			damagedHousingRepo.Update(article);
			cmsSearchService.Restore(Id, CmsSearchType.Article);

			UnitOfWork.Commit();
		}

		public void Remove(Int32 Id)
		{
			var article = damagedHousingRepo.GetById(Id);

			cmsSearchService.Remove(Id, CmsSearchType.Article);
			damagedHousingRepo.Delete(article);

			UnitOfWork.Commit();
		}

		private List<CheckedListItem> GetCategoryCheckedList(List<CheckedListItem> targetList, List<DamagedHousingCategory> levelList, List<DamagedHousingCategory> sourceList, int level = 0, int? userId = null, int? roleId = null)
		{
			targetList = targetList ?? new List<CheckedListItem>();
			var user = userRepo.GetById(userId ?? 0);

			foreach(var cat in levelList)
			{
				targetList.Add(
					new CheckedListItem()
					{
						Value = cat.Id,
						Name = cat.GetLocalValue("Title"),
						Level = level,
						Allowed = user != null ? (cat.AllowedUsers.Any(x => x.Id == userId.Value) || cat.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).Contains(r.Id))) : true
					});

				if(sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
				{
					GetCategoryCheckedList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1, userId);
				}
			}

			return targetList;
		}
	}
}

