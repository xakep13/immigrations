using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public abstract class BaseAuditionEntity : BaseEntity<int>
    {
        public string TitleRu { get; set; }
        public string TitleUk { get; set; }
        public string TitleEn { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? LastEditDate { get; set; }

        public int? CreateUserId { get; set; }
        public int? LastEditUserId { get; set; }
        public bool Published { get; set; }

        public virtual CmsUser CreateUser { get; set; }
        public virtual CmsUser LastEditUser { get; set; }
    }
}