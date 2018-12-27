﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Bissoft.CouncilCMS.Core;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
using xTab.Tools.Attributes;

namespace Bissoft.CouncilCMS.BLL.ViewModels
{
   public class AllowedRole
    {     
        public int RoleId { get; set; }
        public string Name { get; set; }        
        public int ItemId { get; set; }
    }
}
