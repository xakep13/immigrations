using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class DocCategory : BaseCategoryEntity
    {
        public virtual DocCategory ParentCategory { get; set; }
        public virtual DocCategory RelatedCategory { get; set; }
        public virtual DocCategoryTemplate Template { get; set; }
        public virtual ICollection<DocCategory> ChildCategories { get; set; } = new List<DocCategory>();
        public virtual ICollection<DocCategory> RelatedCategories { get; set; } = new List<DocCategory>();
        public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();         
    }
}