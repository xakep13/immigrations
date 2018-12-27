using System;
using System.Configuration;
using System.Web.Mvc;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.Services;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Filters;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    [CmsAuthorize]
    public class PersonsController : BaseCmsController
    {
        // GET: ControlPanel/Person
        private PersonAdminService personService;

        public PersonsController() : base()
        {
            personService = new PersonAdminService(this.ConnectionString);
        }

        public ActionResult List(string query = null, string dateRange = null, int dateType = 0, int? categoryId = null, int itemState = 1, int page = 1, String sortBy = "TitleUk", int sortDirection = 1, int? user = null, int userAction = 0, bool ajaxMode = false)
        {
            if (User.HasPremissions("persons"))
            {
                var userId = User.HasPremission("cr_persons_full") ? null : (int?)User.Identity.UserId;

                if (ajaxMode)
                {
                    var count = 0;
                    var model = personService.GetList(
                        query: query,
                        page: page,
                        categoryId: categoryId,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        itemState: (CmsItemState)itemState,
                        userId: userId,
                        user: user,
                        userAction: userAction,
                        count: out count);

                    ViewBag.Count = count;
                    ViewBag.Page = page;
                    ViewBag.PerPage = 20;

                    return PartialView("_prtPersonList", model);
                }
                else
                {
                    var model = personService.List(
                        query: query,
                        page: page,
                        categoryId: categoryId,
                        dateRange: dateRange,
                        dateType: dateType,
                        sortBy: sortBy,
                        sortDir: sortDirection,
                        user: user,
                        userAction: userAction,
                        itemState: (CmsItemState)itemState,
                        userId: userId);

                    return View(model);
                }
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult GetForm(Int32 Id = 0)
        {
            var userId = User.HasPremission("cr_persons_full") ? null : (int?)User.Identity.UserId;

            var model = personService.GetForm(Id, userId);

            return PartialView("_prtPersonEdit", model);
        }

        public ActionResult PagePrev(Int32 id = 0)
        {
            var model = personService.GetForm(id);
            return PartialView("_prtPersonPrev", model);
        }

        public ActionResult Save(PersonEdit model)
        {
            personService.Save(model);

            return PartialView("_prtPersonEditComplete", model);
        }

        public ActionResult Delete(Int32 Id)
        {
            personService.Delete(Id);

            return null;
        }

        public ActionResult Restore(Int32 Id)
        {
            personService.Restore(Id);

            return null;
        }

        public ActionResult GetCategory(Int32 PersonId, Int32 CategoryId)
        {
            var result = personService.GetCategory(CategoryId, PersonId);

            if (result != null)
                return PartialView("_prtPersonCategoryItemEdit", result);
            else
                return null;
        }

        public ActionResult SaveCategory(PersonCategoryItem model)
        {
            var result = personService.SaveCategory(model);

            if (result != null)
                return PartialView("_prtPersonCategoryItem", result);
            else
                return null;
        }

        public ActionResult DeleteCategory(Int32 PersonId, Int32 CategoryId)
        {
            personService.DeleteCategory(CategoryId, PersonId);
            return null;
        }

        public ActionResult AddUser(AllowedUser model)
        {
            var result = personService.AddUser(model);

            if (result != null)
                return PartialView("_prtAllowedUserItem", result);
            else
                return null;
        }

        public ActionResult DeleteUser(Int32 ItemId, Int32 UserId)
        {
            personService.DeleteUser(ItemId, UserId);
            return null;
        }
    }
}