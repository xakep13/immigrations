using System.Collections.Generic;
using System.Web.Mvc;


namespace Bissoft.CouncilCMS.BLL.Helpers
{
    public static class ValueHelper
    {
        public static List<SelectListItem> OpenDataRefreshSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Каждый день", Value = "Every day" },
            new SelectListItem() { Text = "Каждую неделю", Value = "Every week" },
            new SelectListItem() { Text = "Каждый месяц", Value = "Every month" },
        };

        public static List<SelectListItem> OpenDataCategoriesSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Транспорт", Value = "Транспорт" },
            new SelectListItem() { Text = "Держава", Value = "Держава" },
            new SelectListItem() { Text = "Фінанси", Value = "Фінанси" },
            new SelectListItem() { Text = "Юстиція", Value = "Юстиція" },
            new SelectListItem() { Text = "Податки", Value = "Податки" },
            new SelectListItem() { Text = "Екологія", Value = "Екологія" },
            new SelectListItem() { Text = "Будівництво", Value = "Будівництво" },
            new SelectListItem() { Text = "Земля", Value = "Земля" },
            new SelectListItem() { Text = "Сільске господарство", Value = "Сільске господарство" },
            new SelectListItem() { Text = "Охорона здоров'я", Value = "Охорона здоров'я" },
            new SelectListItem() { Text = "Соціальний захист", Value = "Соціальний захист" },
            new SelectListItem() { Text = "Освіта та культура", Value = "Освіта та культура" },
            new SelectListItem() { Text = "Молодь та спорт", Value = "Молодь та спорт" },
            new SelectListItem() { Text = "Стандарти", Value = "Стандарти" },
            new SelectListItem() { Text = "Економіка", Value = "Економіка" }
        };

        public static List<SelectListItem> BackgroundSizeSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Не обрано", Value = "" },
            new SelectListItem() { Text = "Не змінювати", Value = "auto" },
            new SelectListItem() { Text = "Заповнити блок", Value = "cover" },
            new SelectListItem() { Text = "Вписати в блок", Value = "contain" }
        };

        public static List<SelectListItem> BackgroundRepeatSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Не обрано", Value = "" },
            new SelectListItem() { Text = "Не повторювати", Value = "no-repeat" },
            new SelectListItem() { Text = "Повторювати", Value = "repeat" },
            new SelectListItem() { Text = "Повторювати горизонтально", Value = "repeat-x" },
            new SelectListItem() { Text = "Повторювати вертикально", Value = "repeat-y" },
        };

        public static List<SelectListItem> BackgroundPositionSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Не обрано", Value = "" },
            new SelectListItem() { Text = "Вгорі, ліворуч", Value = "top left" },
            new SelectListItem() { Text = "Вгорі, посередені", Value = "top center" },
            new SelectListItem() { Text = "Вгорі, праворуч", Value = "top right" },
            new SelectListItem() { Text = "По середені, ліворуч", Value = "center left" },
            new SelectListItem() { Text = "По середені", Value = "center" },
            new SelectListItem() { Text = "По середені, праворуч", Value = "center right" },
            new SelectListItem() { Text = "Знизу, ліворуч", Value = "bottom left" },
            new SelectListItem() { Text = "Знизу, посередені", Value = "bottom center" },
            new SelectListItem() { Text = "Знизу, праворуч", Value = "right center" },
        };

        public static List<SelectListItem> TextAlignSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Зліва", Value = "left" },
            new SelectListItem() { Text = "Справа", Value = "right" },
            new SelectListItem() { Text = "По центру", Value = "center" }
        };

        public static List<SelectListItem> FontWeightSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Тонкий", Value = "left" },
            new SelectListItem() { Text = "Звичайний", Value = "right" },
            new SelectListItem() { Text = "Жирний", Value = "center" }
        };

        public static List<SelectListItem> PageSortSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "За датою створення", Value = "create-date" },
            new SelectListItem() { Text = "За датою редагування", Value = "edit-date" },
            new SelectListItem() { Text = "За назвою", Value = "name" }
        };

        public static List<SelectListItem> ArticleSortSelect = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "За датою публікації", Value = "publish-date" },
            new SelectListItem() { Text = "За датою створення", Value = "create-date" },
            new SelectListItem() { Text = "За датою оновлення", Value = "edit-date" },
            new SelectListItem() { Text = "За назвою", Value = "name" }
        };
    }
}
