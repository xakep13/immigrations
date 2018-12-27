﻿using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsDocListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string PublishDate { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime? AcceptDate { get; set; }

        public int For { get; set; }
        public int Against { get; set; }
        public int Abstained { get; set; }
        public int NotVote { get; set; }

        public VoteResult Result { get; set; }

        public int ViewCount { get; set; }

        /* added by vadym */

        public List<CmsDocListItem> Children { get; set; }
        public int? ParentItemId { get; set; }
        public bool HasChilds { get; set; }
        public CmsDocListItem()
        {
            Children = new List<CmsDocListItem>();
        }
    }
}