using System;
using System.Collections.Generic;
using Bissoft.CouncilCMS.Core.Enums;
using Bissoft.CouncilCMS.Core.Globalization;
namespace Bissoft.CouncilCMS.BLL.ViewModels
{
    public class CmsDoc
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string SessionTitle { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int RelatedCategoryId { get; set; }
        public string RelatedCategoryUrl { get; set; }
        public string RelatedCategoryName { get; set; }

        public string Text { get; set; }

        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
     
        public string PublishDate { get; set; }
        public string LastEditDate { get; set; }

        public DateTime? AcceptDate { get; set; }
        public DateTime? PostDate { get; set; }

        public int ViewCount { get; set; }

        public int For { get; set; }
        public int Against { get; set; }
        public int Abstained { get; set; }
        public int NotVote { get; set; }

        public VoteResult Result { get; set; }


        public List<CmsEnterpriseListItem> Enterprises { get; set; }
        public List<CmsPersonListItem> Persons { get; set; }

        public List<CmsDocFile> Files { get; set; }

        public CmsMenu DocMenu { get; set; }
    }

    public class CmsDocFile
    {
        public int Id { get; set; }

        public string Extension { get; set; }

        public int Size { get; set; }

        public string File { get; set; }        

        public string Title { get; set; }        
    }
}
