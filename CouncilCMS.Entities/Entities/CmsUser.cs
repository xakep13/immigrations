using Bissoft.CouncilCMS.Core.Enums;
using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.DAL.Entities
{
    public class CmsUser : BaseEntity<int>
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int? Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Blocked { get; set; }
        public bool ChangePasswordOnLogin { get; set; }
        public int WrongLoginTries { get; set; }
        public string RestorePasswordToken { get; set; }
        public DateTime? RestorePasswordExpire { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Salt { get; set; }
        public string CID { get; set; }

        public virtual ICollection<CmsRole> Roles { get; set; } = new List<CmsRole>();
        public virtual ICollection<Page> AllowedPages { get; set; } = new List<Page>();
        public virtual ICollection<Person> AllowedPersons { get; set; } = new List<Person>();
        public virtual ICollection<Enterprise> AllowedEnterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<ArticleCategory> AllowedArticleCategories { get; set; } = new List<ArticleCategory>();
        public virtual ICollection<PersonCategory> AllowedPersonCategories { get; set; } = new List<PersonCategory>();
        public virtual ICollection<EnterpriseCategory> AllowedEnterpriseCategories { get; set; } = new List<EnterpriseCategory>();
        public virtual ICollection<DocCategory> AllowedDocCategories { get; set; } = new List<DocCategory>();

        public virtual ICollection<Page> CreatedPages { get; set; } = new List<Page>();
        public virtual ICollection<Article> CreatedArticles { get; set; } = new List<Article>();
        public virtual ICollection<Person> CreatedPersons { get; set; } = new List<Person>();
        public virtual ICollection<Enterprise> CreatedEnterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<Doc> CreatedDocs { get; set; } = new List<Doc>();
        public virtual ICollection<Session> CreatedSessions { get; set; } = new List<Session>();

        public virtual ICollection<Page> LastEditedPages { get; set; } = new List<Page>();
        public virtual ICollection<Article> LastEditedArticles { get; set; } = new List<Article>();
        public virtual ICollection<Person> LastEditedPersons { get; set; } = new List<Person>();
        public virtual ICollection<Enterprise> LastEditedEnterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<Doc> LastEditedDocs { get; set; } = new List<Doc>();
        public virtual ICollection<Session> LastEditedSessions { get; set; } = new List<Session>();
    }
}
