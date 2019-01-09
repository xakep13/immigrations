using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			var category = damagedHousingCatRepo.GetSingle(x => x.UrlName == articleUrl, null, true, true);

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
							IsAnons = x.Categories.Any(c => c.Id == 16)
						}).ToList().Select(x => new CmsDamagedHousingListItem()
						{
							Id = x.Id,
							Title = x.GetLocalValue("Title"),
							Description = x.GetLocalValue("Description"),
							UrlName = x.GetLocalValue("UrlName"),
							PublishDateTime = x.PublishDate,
							PublishDate = DateTimeHelper.DateTimeString(x.PublishDate.Value, format: "dd MMMM yyyy HH:mm"),
							EventDate = x.EventDate,
							CategoryTitle = x.CategoryTitle,
							Image = x.Image,
							IsAnons = x.IsAnons,
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
			var model = new CmsDamagedHousing();
			var item = damagedHousingRepo.GetById(id, asNoTracking: true);
			var category = item.Categories.OrderByDescending(x => x.Priority).ThenBy(x => x.TitleUk).FirstOrDefault() ?? new DamagedHousingCategory() { RelatedCategoryId = 0 };
			var relCategory = category.ParentCategoryId > 0 ? damagedHousingCatRepo.GetById(category.ParentCategoryId ?? 0) : category;

			if(item == null || (userId == null && (!item.Published || item.Deleted || item.PublishDate == null || item.PublishDate > DateTime.Now)))
				return null;

			model.Id = item.Id;
			model.Url = item.GetLocalValue("UrlName");
			model.CategoryId = category.Id;
			model.CategoryUrl = category.UrlName;
			model.CategoryName = category.GetLocalValue("Title");
			model.RelatedCategoryId = relCategory.Id;
			model.RelatedCategoryUrl = relCategory.UrlName;
			model.Description = item.GetLocalValue("Description");
			model.EventDate = DateTimeHelper.NullableDateTimeString(item.EventDate);
			model.Image = item.Image;
			model.MetaDescription = item.GetLocalValue("MetaDescriptionRu");
			model.MetaKeywords = item.GetLocalValue("MetaKeywords");
			model.MetaTitle = item.GetLocalValue("MetaTitle");
			model.PublishDate = item.ShowPublihDate ? DateTimeHelper.NullableDateTimeString(item.PublishDate) : String.Empty;
			model.LastEditDate = item.ShowEditDate ? DateTimeHelper.NullableDateTimeString(item.EditedDate) : String.Empty;
			model.Title = item.GetLocalValue("Title");
			model.DamagedHousingMenu = category.SidebarMenuId > 0 ? menuService.GetMenu(category.SidebarMenuId.Value, Mode) : null;
			model.ContentRows = contentService.GetContentRows(item.ContentRows);

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
						Id = x.Id,
						UrlName = x.UrlNameRu,
						UrlNameUk = x.UrlNameUk,
						TitleRu = x.TitleRu,
						TitleUk = x.TitleUk,
						TitleEn = x.TitleEn,
						DescriptionRu = x.DescriptionRu,
						DescriptionUk = x.DescriptionUk,
						DescriptionEn = x.DescriptionEn,
						Image = x.Image,
						ViewCount = x.ViewCount,
						CategoryTitle = x.Categories.OrderByDescending(c => c.Priority).FirstOrDefault()?.TitleUk,
						PublishDate = x.PublishDate,
						EventDate = x.EventDate,
						IsAnons = x.Categories.Any(c => c.Id == 16)
					}).ToList().Select(x => new CmsDamagedHousingListItem()
					{
						Id = x.Id,
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

		public void AddView(Int32 id)
		{
			damagedHousingRepo.Context.ExecuteSqlCommand("UPDATE DamagedHousings SET ViewCount = ViewCount + 1 WHERE Id = {0}", false, null, id);
		}
	}
}
