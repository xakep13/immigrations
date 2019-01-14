using Bissoft.CouncilCMS.BLL.Services.Public;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.DAL.Entities;
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
	public class CmsDamagedHousingService : BaseService
	{
		private IRepository<DamagedHousing, int> damagedHousingRepo;
		private IRepository<DamagedHousingCategory, int> damagedHousingCatRepo;
		private CmsContentService contentService;
		private CmsSearchService cmsSearchService;
		private CmsMenuService menuService;

		public CmsDamagedHousingService(string ConnectionString) : base(ConnectionString)
		{
			Initialize();
		}

		public CmsDamagedHousingService(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			Initialize();
		}

		private void Initialize()
		{
			damagedHousingRepo = UnitOfWork.GetIntRepository<DamagedHousing>();
			damagedHousingCatRepo = UnitOfWork.GetIntRepository<DamagedHousingCategory>();
			contentService = new CmsContentService(this.UnitOfWork);
			cmsSearchService = new CmsSearchService(this.UnitOfWork);
			menuService = new CmsMenuService(this.UnitOfWork);
		}

		public CmsDamagedHousingList CategoryList(string articleUrl, ShowMenuItemMode Mode, string query = null, string date = null, int page = 1, int perPage = 20, int? excludeId = null, string excludeCategory = null)
		{
			if(string.IsNullOrEmpty(articleUrl))
				articleUrl = "all-houses";

			var category = damagedHousingCatRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);
			var childlcat = category.ChildCategories;

			if(category != null)
			{
				if(category.Template == null || category.Template.Name != "news-by-days")
				{
					var relCategory = damagedHousingCatRepo.GetById(category.RelatedCategoryId ?? 0, asNoTracking: true) ?? new DamagedHousingCategory();
					var now = DateTime.Now;
					var count = 0;

					var predicate = PredicateBuilder.True<DamagedHousing>();

					if(!String.IsNullOrEmpty(query))
					{
						var ids = cmsSearchService.Find(query, CmsSearchType.DamagedHousing);

						if(ids != null && ids.Count > 0)
						{
							var itemIds = cmsSearchService.GetItemIds(ids);

							predicate = predicate.And(x => itemIds.Contains(x.Id));
						}
					}

					if(!String.IsNullOrEmpty(date))
					{
						var fromDateParse = DateTimeHelper.ParseDateNullable(date, null);

						if(fromDateParse != null)
						{
							var fromDate = fromDateParse.Value.Date;
							var toDate = fromDateParse.Value.AddDays(1).AddSeconds(-1);

							predicate = predicate.And(x => (x.PublishDate >= fromDate && x.PublishDate <= toDate) || (x.EventDate >= fromDate && x.EventDate <= toDate));
						}
					}

					if(excludeId > 0)
					{
						predicate = predicate.And(x => x.Id != excludeId.Value);
					}

					if(!String.IsNullOrEmpty(excludeCategory))
					{
						predicate = predicate.And(x => !x.Categories.Any(c => c.UrlName == excludeCategory));
					}

					predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

					var list = damagedHousingRepo
						.GetList(out count,
						predicate,
						x => x.OrderByDescending(o => o.PublishDate),
						"Categories", (page - 1) * perPage, perPage).Select(x =>
						new
						{
							x.Id,

							x.Adress,
							x.Email,
							x.FullName,
							x.StartWork,
							x.EndWork,
							x.Status,
							x.FinanceType,
							x.FinanceSource,
							x.Year,

							UrlName = x.UrlNameRu,
							x.UrlNameUk,
							x.TitleRu,
							x.TitleUk,
							x.TitleEn,
							x.DescriptionRu,
							x.DescriptionUk,
							x.DescriptionEn,
							x.Image,
							x.ViewCount,
							x.EventDate,
							x.PublishDate,
							CategoryTitle = x.Categories.OrderByDescending(c => c.Priority).FirstOrDefault()?.TitleUk,
						}).ToList().Select(x => new CmsDamagedHousingListItem()
						{
							Id = x.Id,
							Address = x.Adress,
							Title = x.GetLocalValue("Title"),
							Description = x.GetLocalValue("Description"),
							UrlName = x.GetLocalValue("UrlName"),
							PublishDateTime = x.PublishDate,
							PublishDate = DateTimeHelper.DateTimeString(x.PublishDate.Value, format: "dd MMMM yyyy HH:mm"),
							EventDate = x.EventDate,
							CategoryTitle = x.CategoryTitle,
							Image = x.Image,
							ViewCount = x.ViewCount,
						}).ToList();

					var model = new CmsDamagedHousingList()
					{
						Page = page,
						PerPage = perPage,
						Count = count,
						CategoryId = category.Id,
						CategoryName = category.GetLocalValue("Title"),
						CategoryDescription = category.GetLocalValue("Description"),
						CategoryUrl = category.UrlName,
						RelatedCategoryId = relCategory.Id,
						RelatedCategoryName = relCategory.GetLocalValue("Title"),
						RelatedCategoryUrl = relCategory.UrlName,
						TemplateId = category.TemplateId ?? 0,
						TemplateName = category.Template != null ? category.Template.Name : string.Empty,
						ShowSearchForm = category.ShowSearchForm,
						DamagedHousings = list,
						CategoryMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null
					};

					return model;
				}
				else
				{
					var relCategory = damagedHousingCatRepo.GetById(category.RelatedCategoryId ?? 0) ?? new DamagedHousingCategory();
					var now = DateTime.Now;

					var dates = damagedHousingRepo.GetList(filter: x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now).Select(x => x.PublishDate.Value).ToList().Select(x => x.Date).Distinct().OrderByDescending(d => d).ToList();
					var count = dates.Count();

					var curDates = dates.Skip((page - 1) * 3).Take(3).OrderBy(x => x.Date).ToList();

					if(curDates != null && curDates.Count() > 0)
					{
						var fromDate = curDates.First().Date;
						var toDate = curDates.Last().AddDays(1).AddTicks(-1);

						var predicate = PredicateBuilder.True<DamagedHousing>();

						predicate = predicate.And(x => x.PublishDate >= fromDate && x.PublishDate <= toDate && x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null);

						if(!String.IsNullOrEmpty(excludeCategory))
						{
							predicate = predicate.And(x => !x.Categories.Any(c => c.UrlName == excludeCategory));
						}

						var list = damagedHousingRepo
							.GetList(
							predicate,
							x => x.OrderByDescending(o => o.PublishDate)).Select(x =>
							new
							{
								x.Id,

								x.Adress,
								x.Email,
								x.FullName,
								x.StartWork,
								x.EndWork,
								x.Status,
								x.FinanceType,
								x.FinanceSource,
								x.Year,

								UrlName = x.UrlNameRu,
								x.UrlNameUk,
								x.TitleRu,
								x.TitleUk,
								x.TitleEn,
								x.DescriptionRu,
								x.DescriptionUk,
								x.DescriptionEn,
								x.Image,
								x.PublishDate,
								x.EventDate,
							}).ToList().Select(x => new CmsDamagedHousingListItem()
							{
								Id = x.Id,
								Address = x.Adress,
								Title = x.GetLocalValue("Title"),
								Description = x.GetLocalValue("Description"),
								UrlName = x.GetLocalValue("UrlName"),
								PublishDateTime = x.PublishDate,
								PublishDate = DateTimeHelper.DateTimeString(x.PublishDate.Value, format: "dd MMMM yyyy HH:mm"),
								EventDate = x.EventDate,
								Image = x.Image,
							}).ToList();

						var model = new CmsDamagedHousingList()
						{
							Page = page,
							PerPage = 3,
							Count = count,
							CategoryId = category.Id,
							CategoryName = category.GetLocalValue("Title"),
							CategoryDescription = category.GetLocalValue("Description"),
							CategoryUrl = category.UrlName,
							RelatedCategoryId = relCategory.Id,
							RelatedCategoryName = relCategory.GetLocalValue("Title"),
							RelatedCategoryUrl = relCategory.UrlName,
							TemplateId = category.TemplateId ?? 0,
							TemplateName = category.Template != null ? category.Template.Name : string.Empty,
							ShowSearchForm = category.ShowSearchForm,
							DamagedHousings = list,
							CategoryMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null
						};

						return model;
					}
				}
			}
			return null;
		}

		public CmsDamagedHousing Article(Int32 id, ShowMenuItemMode Mode, int? userId = null)
		{
			var item = damagedHousingRepo.GetById(id, asNoTracking: true);
			var category = item.Categories.Where(x=>x.UrlName!="all-houses");
			//var relCategory = category.ParentCategoryId > 0 ? damagedHousingCatRepo.GetById(category.ParentCategoryId ?? 0) : category;

			if(item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
				return null;

			var model = new CmsDamagedHousing
			{
				Id = item.Id,
				Year = item.Year,
				FinanceSource = item.FinanceSource,
				FinanceType = item.FinanceType,
				Status = item.Status,
				EndWork = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.EndWork) : String.Empty,
				StartWork = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.StartWork) : String.Empty,
				FullName = item.FullName,
				Email = item.Email,
				Adress = item.Adress,
				Lat = item.Lat,
				Lng = item.Lng,
				Price = item.Price,

				Url = item.GetLocalValue("UrlName"),
				//CategoryId = category.Id,
				//CategoryUrl = category.UrlName,
				CategoryName = category.Select(x => x.TitleUk).ToList(),
				//RelatedCategoryId = relCategory.Id,
				//RelatedCategoryUrl = relCategory.UrlName,
				Description = item.GetLocalValue("Description"),
				EventDate = DateTimeHelper.NullableDateTimeString(item.EventDate),
				Image = item.Image,
				MetaDescription = item.GetLocalValue("MetaDescriptionRu"),
				MetaKeywords = item.GetLocalValue("MetaKeywords"),
				MetaTitle = item.GetLocalValue("MetaTitle"),
				PublishDate = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.PublishDate) : String.Empty,
				LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.EditedDate) : String.Empty,
				Title = item.GetLocalValue("Title"),
				//DamagedHousingMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null,
				ContentRows = contentService.GetContentRows(item.ContentRows)
			};

			AddView(id);

			return model;
		}

		public CmsDamagedHousing LastArticleInCategory(Int32 id, Boolean withContent = false)
		{
			var category = damagedHousingCatRepo.GetById(id);

			var date = DateTime.Now;

			var item = damagedHousingRepo.GetList(
				x => x.Categories.Any(c => x.Id == id) && x.Published && x.PublishDate != null && x.PublishDate <= date,
				x => x.OrderByDescending(o => o.PublishDate), (withContent ? "ContentRows" : null),
				0, 1).FirstOrDefault();

			if(item != null)
			{
				var model = new CmsDamagedHousing()
				{
					Id = item.Id,
					Url = item.GetLocalValue("UrlName"),
					Description = item.GetLocalValue("Description"),
					EventDate = item.EventDate.ToString(),
					Image = item.Image,
					PublishDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
					Title = item.GetLocalValue("Title"),
					ContentRows = withContent ? contentService.GetContentRows(item.ContentRows) : null,
					ViewCount = item.ViewCount
				};

				return model;
			}
			else
			{
				return null;
			}
		}

		public CmsDamagedHousing LastArticleInCategory(String url, Boolean withContent = false)
		{
			var category = damagedHousingCatRepo.GetSingle(x => x.UrlName == url, null, true, true);

			var date = DateTime.Now;

			var item = damagedHousingRepo.GetList(
				x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= date,
				x => x.OrderByDescending(o => o.PublishDate), "Categories" + (withContent ? ",ContentRows" : null)).FirstOrDefault();

			if(item != null)
			{
				var model = new CmsDamagedHousing()
				{
					Id = item.Id,
					Url = item.GetLocalValue("UrlName"),
					Description = item.GetLocalValue("Description"),
					Image = item.Image,
					PublishDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
					EventDate = DateTimeHelper.NullableDateTimeString(item.PublishDate, Format: "dd MMMM yyyy HH:mm"),
					Title = item.GetLocalValue("Title"),
					ContentRows = withContent ? contentService.GetContentRows(item.ContentRows) : null,
					ViewCount = item.ViewCount,
				};

				return model;
			}
			else
			{
				return null;
			}
		}

		public CmsDamagedHousingList LastArticles(string articleUrl, int limit = 5, int? excludeId = null)
		{
			var category = damagedHousingCatRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

			if(category != null)
			{
				var now = DateTime.Now;
				var count = 0;

				var predicate = PredicateBuilder.True<DamagedHousing>();

				if(excludeId > 0)
					predicate = predicate.And(x => x.Id != excludeId.Value);

				predicate = predicate.And(x => x.Categories.Any(c => c.Id == category.Id) && x.Published && x.PublishDate != null && x.PublishDate <= now);

				var list = damagedHousingRepo
					.GetList(out count,
					predicate,
					x => x.OrderByDescending(o => o.PublishDate),
					"Categories", 0, limit).Select(x =>
					new
					{
						x.Id,
						x.Adress,
						x.Email,
						x.FullName,
						x.StartWork,
						x.EndWork,
						x.Status,
						x.FinanceType,
						x.FinanceSource,
						x.Year,
						UrlName = x.UrlNameUk,
						x.UrlNameUk,
						x.TitleRu,
						x.TitleUk,
						x.TitleEn,
						x.DescriptionRu,
						x.DescriptionUk,
						x.DescriptionEn,
						x.Image,
						x.ViewCount,
						CategoryTitle = x.Categories.OrderByDescending(c => c.Priority).FirstOrDefault()?.TitleUk,
						x.PublishDate,
						x.EventDate,
						IsAnons = x.Categories.Any(c => c.Id == 16)
					}).ToList().Select(x => new CmsDamagedHousingListItem()
					{
						Id = x.Id,
						Address = x.Adress,
						Title = x.GetLocalValue("Title"),
						Description = x.GetLocalValue("Description"),
						UrlName = x.GetLocalValue("UrlName"),
						PublishDate = DateTimeHelper.NullableDateTimeString(x.PublishDate, Format: "dd MMMM yyyy HH:mm"),
						EventDate = x.EventDate,
						PublishDateTime = x.PublishDate,
						Image = x.Image,
						IsAnons = x.IsAnons,
						ViewCount = x.ViewCount,
						CategoryTitle = x.CategoryTitle
					}).ToList();

				var model = new CmsDamagedHousingList()
				{
					CategoryId = category.Id,
					CategoryName = category.GetLocalValue("Title"),
					CategoryDescription = category.GetLocalValue("Description"),
					CategoryUrl = category.UrlName,
					TemplateId = category.TemplateId ?? 0,
					TemplateName = category.Template != null ? category.Template.Name : string.Empty,
					DamagedHousings = list
				};

				return model;
			}

			return new CmsDamagedHousingList();
		}

		public CalendarWidget GetEventCalendar(string url)
		{
			var now = DateTime.Now;
			var category = damagedHousingCatRepo.GetSingle(x => x.UrlName == url, null, true, true);

			var firstDate = damagedHousingRepo.DbSet.Where(x => !x.Deleted && x.Published && x.PublishDate <= now && x.EventDate != null && x.Categories.Any(c => c.Id == category.Id)).Min(x => x.EventDate);
			var lastDate = damagedHousingRepo.DbSet.Where(x => !x.Deleted && x.Published && x.PublishDate <= now && x.EventDate != null && x.Categories.Any(c => c.Id == category.Id)).Max(x => x.EventDate);
			var dates = damagedHousingRepo.DbSet.Where(x => !x.Deleted && x.Published && x.PublishDate <= now && x.EventDate != null && x.Categories.Any(c => c.Id == category.Id)).OrderByDescending(x => x.EventDate).Select(x => x.EventDate).ToList();

			var model = new CalendarWidget()
			{
				FirstDate = firstDate.Value,
				LastDate = lastDate.Value,
				Events = dates.Select(x => x.Value).ToList()
			};

			return model;
		}

		public DamagedHousingEdit GetForEdit()
		{
			var categories = damagedHousingCatRepo.GetList(includeProperties: "AllowedUsers,AllowedRoles").ToList();

			var article = new DamagedHousing()
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
					0);

			foreach(var item in article.Categories.Select(x => x.Id).ToList())
			{
				model.DamagedHousingCategories.Where(x => x.Value == item).FirstOrDefault().IsChecked = true;
			}


			return model;
		}

		private List<CheckedListItem> GetCategoryCheckedList(List<CheckedListItem> targetList, List<DamagedHousingCategory> levelList, List<DamagedHousingCategory> sourceList, int level = 0, int? userId = null, int? roleId = null)
		{
			targetList = targetList ?? new List<CheckedListItem>();


			foreach(var cat in levelList)
			{
				targetList.Add(
					new CheckedListItem()
					{
						Value = cat.Id,
						Name = cat.GetLocalValue("Title"),
						Level = level,
						Allowed = true
					});

				if(sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
				{
					GetCategoryCheckedList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1, userId);
				}
			}

			return targetList;
		}

		public void Save(DamagedHousingEdit model)
		{
			model.TitleRu = String.IsNullOrEmpty(model.TitleRu) ? "Без названия" : model.TitleRu;
			model.TitleUk = String.IsNullOrEmpty(model.TitleUk) ? "Без назви" : model.TitleUk;
			model.TitleEn = String.IsNullOrEmpty(model.TitleEn) ? "No name" : model.TitleEn;

			var item = damagedHousingRepo.GetById(model.Id);

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
			item.Year = model.Year;
			item.Price = model.Price;
			item.Lat = model.Lat;
			item.Lng = model.Lng;

			item.Published = model.Published;
			item.Image = model.Image;
			item.LastEditDate = DateTime.Now;
			item.EditedDate = DateTimeHelper.ParseDateNullable(model.EditedDate, DateTime.Now, DateTimeHelper.DefaultTimeOfDay.None);
			item.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
			item.EventDate = DateTimeHelper.ParseDateNullable(model.EventDate, null, DateTimeHelper.DefaultTimeOfDay.None);
			item.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;

			if(item.Categories != null)
				item.Categories.Clear();
			else
				item.Categories = new List<DamagedHousingCategory>();

			damagedHousingRepo.Update(item);

			if(model.DamagedHousingCategories != null)
				foreach(var cat in model.DamagedHousingCategories)
				{
					if(cat.IsChecked)
						item.Categories.Add(damagedHousingCatRepo.GetById(cat.Value));
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
		}

		public CmsDamagedHousingCategoryList GetChildrenCategory(string parent)
		{
			var model = damagedHousingCatRepo.GetSingle(x => x.UrlName == parent);

			CmsDamagedHousingCategoryList ChildCategories = new CmsDamagedHousingCategoryList
			{
				Id = model.Id,
				Title = model.TitleUk,
				Categories = model.ChildCategories.Select(x => new CmsDamagedHousingCategoryListItem
				{
					Id = x.Id,
					Title = x.TitleUk,
					UrlName = x.UrlName
				}).ToList()
			};
			return ChildCategories;
		}

		public int HousCount()
		{
			return damagedHousingRepo.GetList().Count();
		}

		public void AddView(Int32 id)
		{
			damagedHousingRepo.Context.ExecuteSqlCommand("UPDATE DamagedHousings SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
		}
	}
}
