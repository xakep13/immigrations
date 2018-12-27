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
using System.Web;
using Bissoft.CouncilCMS.Web.Core;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class DocAdminService : BaseService
    {
        private IRepository<Doc, int> docRepo;
        private IRepository<DocFile, int> docFileRepo;
        private IRepository<DocCategory, int> categoryRepo;
        private IRepository<Person, int> personRepo;
        private IRepository<Enterprise, int> enterpriseRepo;
        private IRepository<CmsUser, int> userRepo;
        private SelectListService selectListService;

        public DocAdminService(string ConnectionString) : base(ConnectionString)
        {
            Initialize();
        }

        public DocAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Initialize();
        }

        private void Initialize()
        {
            docRepo = UnitOfWork.GetRepository<Doc, int>();
            docFileRepo = UnitOfWork.GetRepository<DocFile, int>();
            categoryRepo = UnitOfWork.GetRepository<DocCategory, int>();
            personRepo = UnitOfWork.GetRepository<Person, int>();
            userRepo = UnitOfWork.GetIntRepository<CmsUser>();
            enterpriseRepo = UnitOfWork.GetRepository<Enterprise, int>();
            selectListService = new SelectListService(this.UnitOfWork);
        }

        public DocList List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int? typeId = null, CmsItemState itemState = CmsItemState.All, int page = 1, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var model = new DocList()
            {
                Page = page,
                Query = query,
                DateRange = dateRange,
                DateType = dateType,
                CategoryId = categoryId,
                TypeId = typeId,
                ItemState = (int)itemState,
                SortBy = sortBy,
                SortDirection = sortDir,
                Categories = selectListService.CategorySelectList<DocCategory>(String.Empty, userId),
                Types = selectListService.GetDocTypeSelectList(String.Empty),
                User = user,
                UserAction = userAction,
                Users = selectListService.GetCmsUserSelectList(String.Empty)
            };

            return model;
        }
        public List<DocListItem> GetList(out int count, string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int? typeId = null, CmsItemState itemState = CmsItemState.All, int page = 1, int perPage = 20, string sortBy = "PublishDate", int sortDir = 1, int? user = null, int userAction = 0, int? userId = null)
        {
            var predicate = CreateStatePredicate<Doc>(itemState);

            if (!String.IsNullOrEmpty(query))
                predicate = TitleFilter(predicate, query);

            if (!String.IsNullOrEmpty(dateRange))
            {
                if (dateType < 2)
                    predicate = DateRangeFilter(predicate, dateRange, dateType);
                else
                {
                    var dates = dateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                    if (dates.Length == 2)
                    {
                        var fromDate = DateTimeHelper.ParseDateNullable(dates[0]);
                        var toDate = DateTimeHelper.ParseDateNullable(dates[1]);

                        if (fromDate != null && toDate != null)
                        {
                            fromDate = fromDate.Value.Date;
                            toDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);

                            predicate = predicate.And(x => x.AcceptDate >= fromDate && x.AcceptDate <= toDate);                            
                        }
                    }
                }
            }
                

            if (user > 0)
                predicate = UserFilter(predicate, user, userAction);

            if (categoryId > 0)
                predicate = predicate.And(x => x.Categories.Any(c => c.Id == categoryId));

            if (typeId > 0)
                predicate = predicate.And(x => x.DocTypeId == typeId.Value);          

            if (userId > 0)
            {
                var roles = userRepo.GetById(userId.Value, "Roles", true).Roles.Select(x => x.Id);
                predicate = predicate.And(x => x.Categories.Any(c => c.AllowedUsers.Any(u => u.Id == userId)) || x.Categories.Any(c => c.AllowedRoles.Any(r => roles.Contains(c.Id))));
            }

            var list = docRepo.GetList(out count, predicate, x => x.OrderByColumn(sortBy, sortDir > 0), "DocType,Categories,CreateUser,LastEditUser", (page - 1) * perPage, perPage, true, false);

            var model = list.Select(x => new DocListItem()
                {
                    Id = x.Id,
                    Number = x.Number,
                    Type = x.DocType.GetLocalValue("Title"),
                    TitleRu = x.TitleRu,
                    TitleUk = x.TitleUk,
                    TitleEn = x.TitleEn,
                    CreateDate = x.CreateDate,
                    LastEditDate = x.LastEditDate,
                    EditedDate = x.EditedDate,
                    PublishDate = x.PublishDate,
                    PostDate = x.AcceptDate,
                    Published = x.Published,
                    Deleted = x.Deleted,
                    ViewCount = x.ViewCount,
                    CategoriesUk = x.Categories.Select(c => c.TitleUk),
                    CategoriesRu = x.Categories.Select(c => c.TitleRu),
                    CategoriesEn = x.Categories.Select(c => c.TitleEn),
                    CreateUser = x.CreateUserId > 0 ? x.CreateUser.Name : null,
                    LastEditUser = x.LastEditUserId > 0 ? x.LastEditUser.Name : null
            }).ToList();
         
            return model;
        }

        public DocEdit GetForEdit(Int32 Id, int? userId = null)
        {
            var categories = categoryRepo.GetList(includeProperties: "AllowedUsers,AllowedRoles").ToList();

            var doc = docRepo.GetById(Id) ?? new Doc()
            {
                Id = 0,
                TitleUk = "Без назви",
                TitleRu = "Без названия",
                TitleEn = "No title",
                Categories = new List<DocCategory>(),
                Persons = new List<Person>(),
                Enterprises = new List<Enterprise>(),
                CreateDate = DateTime.Now,
                PublishDate = DateTime.Now,
                LastEditDate = DateTime.Now,
                ShowPublihDate = true,
                ShowEditDate = true,
                DocTypeId = Int32.Parse(selectListService.GetDocTypeSelectList().FirstOrDefault().Value),
                CreateUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId
            };

            if (Id == 0)
            {
                docRepo.Insert(doc);
                UnitOfWork.Commit();
            }

            var model = new DocEdit();

            model.Id = doc.Id;
            model.Number = doc.Number;

            model.TitleRu = doc.TitleRu;
            model.TitleUk = doc.TitleUk;
            model.TitleEn = doc.TitleEn;

            model.MetaTitleRu = doc.MetaTitleRu;
            model.MetaTitleUk = doc.MetaTitleUk;
            model.MetaTitleEn = doc.MetaTitleEn;
            model.MetaKeywordsRu = doc.MetaKeywordsRu;
            model.MetaKeywordsUk = doc.MetaKeywordsUk;
            model.MetaKeywordsEn = doc.MetaKeywordsEn;
            model.MetaDescriptionRu = doc.MetaDescriptionRu;
            model.MetaDescriptionUk = doc.MetaDescriptionUk;
            model.MetaDescriptionEn = doc.MetaDescriptionEn;

            model.Text = doc.Text;
            model.TypeId = doc.DocTypeId;

            model.KeywordsRu = doc.KeywordsRu;
            model.KeywordsUk = doc.KeywordsUk;
            model.KeywordsEn = doc.KeywordsEn;

            model.Published = doc.Published;
            model.ShowEditDate = doc.ShowEditDate;
            model.ShowPublihDate = doc.ShowPublihDate;
                       
            model.PublishDate = DateTimeHelper.NullableDateTimeString(doc.PublishDate, null, false, "dd.MM.yyyy HH:mm");
            model.AcceptDate = DateTimeHelper.NullableDateTimeString(doc.AcceptDate, null, false, "dd.MM.yyyy HH:mm");
            model.PostDate = DateTimeHelper.NullableDateTimeString(doc.PostDate, null, false, "dd.MM.yyyy HH:mm");

            model.Files = doc.Files != null ?
                doc.Files.Select(x => new DocFileUpload()
                {
                    Id = x.Id,
                    TitleEn = x.TitleEn,
                    TitleRu = x.TitleRu,
                    TitleUk = x.TitleUk,
                    DocId = x.DocId,
                    Extension = x.Extension,
                    File = x.File,
                    Size = (int)x.Size
                }).ToList() : null;

            model.Categories =
                GetCategoryCheckedList(
                    model.Categories,
                    categories.Where(x => x.ParentCategoryId == null).OrderBy(x => x.Position).ToList(),
                    categories,
                    0, userId);

            model.DocPersons = doc.Persons != null ?
                doc.Persons.Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk })
               .Select(x => new DocPerson() { PersonId = x.Id, DocId = Id, Title = x.GetLocalValue("Title") })
               .ToList() : new List<DocPerson>();

            model.DocEnterprises = doc.Enterprises != null ?
                doc.Enterprises.Select(x => new { Id = x.Id, TitleRu = x.TitleRu, TitleUk = x.TitleUk })
               .Select(x => new DocEnterprise() { EnterpriseId = x.Id, DocId = Id, Title = x.GetLocalValue("Title") })
               .ToList() : new List<DocEnterprise>();

            model.Types = selectListService.GetDocTypeSelectList();

            foreach (var item in doc.Categories.Select(x => x.Id).ToList())
            {
                model.Categories.Where(x => x.Value == item).FirstOrDefault().IsChecked = true;
            }

            return model;
        }
        public DocEdit Save(DocEdit model)
        {
            var item = docRepo.GetById(model.Id);

            var lemmatizeProcRu = new LemmaProcessor("ru");
            var lemmatizeProcUk = new LemmaProcessor("uk");
            var lemmatizeProcEn = new LemmaProcessor("en");

            var cleanText =
                model.Number + " " +
                lemmatizeProcUk.PrepareText(model.TitleUk + " " + model.KeywordsUk + " " + AngleSharpParser.RemoveTags(model.Text)) + " " +
                lemmatizeProcRu.PrepareText(model.TitleRu + " " + model.KeywordsRu) + " " +
                lemmatizeProcEn.PrepareText(model.TitleEn + " " + model.KeywordsEn);

            item.Id = model.Id;
            item.Number = model.Number;

            item.TitleRu = model.TitleRu;
            item.TitleUk = model.TitleUk;
            item.TitleEn = model.TitleEn;

            item.MetaTitleRu = model.MetaTitleRu;
            item.MetaTitleUk = model.MetaTitleUk;
            item.MetaTitleEn = model.MetaTitleEn;
            item.MetaKeywordsRu = model.MetaKeywordsRu;
            item.MetaKeywordsUk = model.MetaKeywordsUk;
            item.MetaKeywordsEn = model.MetaKeywordsEn;
            item.MetaDescriptionRu = model.MetaDescriptionRu;
            item.MetaDescriptionUk = model.MetaDescriptionUk;
            item.MetaDescriptionEn = model.MetaDescriptionEn;

            item.Text = model.Text;
            item.CleanText = cleanText;

            item.KeywordsRu = model.KeywordsRu;
            item.KeywordsUk = model.KeywordsUk;
            item.KeywordsEn = model.KeywordsEn;

            item.DocTypeId = model.TypeId;

            item.ShowPublihDate = model.ShowPublihDate;
            item.ShowEditDate = model.ShowEditDate;

            item.Saved = true;
            item.Published = model.Published;
            item.LastEditDate = DateTime.Now;
            item.PublishDate = DateTimeHelper.ParseDateNullable(model.PublishDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.AcceptDate = DateTimeHelper.ParseDateNullable(model.AcceptDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.PostDate = DateTimeHelper.ParseDateNullable(model.PostDate, null, DateTimeHelper.DefaultTimeOfDay.None);
            item.LastEditUserId = (HttpContext.Current.User as CmsPrincipal).Identity.UserId;

            if (item.Categories != null)
                item.Categories.Clear();
            else
                item.Categories = new List<DocCategory>();

            docRepo.Update(item);
            UnitOfWork.Commit();

            foreach (var cat in model.Categories.Where(x => x.IsChecked))
            {
                var category = categoryRepo.GetById(cat.Value);

                if (category != null)
                    item.Categories.Add(category);
            }

            docRepo.Update(item);
            UnitOfWork.Commit();

            return model;
        }

        public void AddFile(DocFileUpload file)
        {
            var newFile = new DocFile()
            {
                TitleEn = file.TitleEn,
                TitleRu = file.TitleRu,
                TitleUk = file.TitleUk,
                File = file.File,
                Size = file.Size,
                Extension = file.Extension,
                DocId = file.DocId
            };

            docFileRepo.Insert(newFile);
            UnitOfWork.Commit();
        }



        public void DeleteFile(Int32 Id)
        {
            var file = docFileRepo.GetById(Id);

            docFileRepo.Delete(file);

            UnitOfWork.Commit();
        }


        public DocPerson AddPerson(Int32 PersonId, Int32 DocId)
        {
            var person = personRepo.GetById(PersonId);
            var doc = docRepo.GetById(DocId);

            if (doc.Persons == null)
                doc.Persons = new List<Person>();

            if (doc != null && person != null && !doc.Persons.Any(x => x.Id == PersonId))
            {
                doc.Persons.Add(person);
                docRepo.Update(doc);
                UnitOfWork.Commit();

                return new DocPerson() { DocId = DocId, PersonId = PersonId, Title = person.GetLocalValue("Title") };
            }

            return null;
        }

        public DocEnterprise AddEnterprise(Int32 EnterpriseId, Int32 DocId)
        {
            var enterprise = enterpriseRepo.GetById(EnterpriseId);
            var doc = docRepo.GetById(DocId);

            if (doc.Enterprises == null)
                doc.Enterprises = new List<Enterprise>();

            if (doc != null && enterprise != null && !doc.Enterprises.Any(x => x.Id == EnterpriseId))
            {
                doc.Enterprises.Add(enterprise);
                docRepo.Update(doc);
                UnitOfWork.Commit();

                return new DocEnterprise() { DocId = DocId, EnterpriseId = EnterpriseId, Title = enterprise.GetLocalValue("Title") };
            }

            return null;
        }

        public void DeletePerson(Int32 PersonId, Int32 DocId)
        {
            var person = personRepo.GetById(PersonId);
            var doc = docRepo.GetById(DocId);

            if (doc != null && person != null && doc.Persons != null && doc.Persons.Any(x => x.Id == PersonId))
            {
                doc.Persons.Remove(person);
            }

            docRepo.Update(doc);
            UnitOfWork.Commit();
        }

        public void DeleteEnterprise(Int32 EnterpriseId, Int32 DocId)
        {
            var enterprise = enterpriseRepo.GetById(EnterpriseId);
            var doc = docRepo.GetById(DocId);

            if (doc.Enterprises == null)
                doc.Enterprises = new List<Enterprise>();

            if (doc != null && enterprise != null && doc.Enterprises != null && doc.Enterprises.Any(x => x.Id == EnterpriseId))
            {
                doc.Enterprises.Remove(enterprise);
            }

            docRepo.Update(doc);
            UnitOfWork.Commit();
        }

        public void Delete(Int32 Id)
        {
            var doc = docRepo.GetById(Id);

            doc.Deleted = true;

            docRepo.Update(doc);

            UnitOfWork.Commit();
        }

        public void Restore(Int32 Id)
        {
            var doc = docRepo.GetById(Id);

            doc.Deleted = false;

            docRepo.Update(doc);

            UnitOfWork.Commit();
        }

        public void Remove(Int32 Id)
        {
            var doc = docRepo.GetById(Id);

            docRepo.Delete(doc);

            UnitOfWork.Commit();
        }

        private List<CheckedListItem> GetCategoryCheckedList(List<CheckedListItem> targetList, List<DocCategory> levelList, List<DocCategory> sourceList, int level = 0, int? userId = null)
        {
            targetList = targetList ?? new List<CheckedListItem>();
            var user = userRepo.GetById(userId ?? 0);

            foreach (var cat in levelList)
            {
                targetList.Add(
                    new CheckedListItem()
                    {
                        Value = cat.Id,
                        Name = cat.GetLocalValue("Title"),
                        Level = level,
                        Allowed = user != null ? (cat.AllowedUsers.Any(x => x.Id == userId.Value) || cat.AllowedRoles.Any(r => user.Roles.Select(x => x.Id).Contains(r.Id))) : true
                    });

                if (sourceList.Count(x => x.ParentCategoryId == cat.Id) > 0)
                {
                    GetCategoryCheckedList(targetList, sourceList.Where(x => x.ParentCategoryId == cat.Id).OrderBy(x => x.Position).ToList(), sourceList, level + 1, userId);
                }
            }

            return targetList;
        }
    }
}