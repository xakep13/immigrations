using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
   public class MenuListItem
   {
       public int Id { get; set; }
       public int Index { get; set; }
       public string NameRu { get; set; }
       public string NameUk { get; set; }
       public string NameEn { get; set; }
       public List<MenuItemListItem> MenuItems { get; set; }
   }
}
