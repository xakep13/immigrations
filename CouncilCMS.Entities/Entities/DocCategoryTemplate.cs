using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class DocCategoryTemplate : BaseCategoryTemplate
    {
        public virtual ICollection<DocCategory> Categories { get; set; } = new List<DocCategory>();
    }
}
