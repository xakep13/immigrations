using System;
using System.Collections.Generic;

namespace Bissoft.CouncilCMS.Models.Entities
{
    public class CmsUser : Entity<int>
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public Gender? Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Blocked { get; set; }
        public bool ChangePasswordOnLogin { get; set; }
        public int WrongLoginTries { get; set; }
        public string RestorePasswordToken { get; set; }
        public DateTime? RestorePasswordExpire { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Salt { get; set; }
        public string CID { get; set; }
        public virtual List<CmsRole> Roles { get; set; }
        public virtual ICollection<Page> AllowedPages { get; set; } = new List<Page>();
        public virtual ICollection<Person> AllowedPersons { get; set; } = new List<Person>();
        public virtual ICollection<Enterprise> AllowedEnterprises { get; set; } = new List<Enterprise>();
        public virtual ICollection<ArticleCategory> AllowedArticleCategories { get; set; } = new List<ArticleCategory>();
        public virtual ICollection<PersonCategory> AllowedPersonCategories { get; set; } = new List<PersonCategory>();
        public virtual ICollection<EnterpriseCategory> AllowedEnterpriseCategories { get; set; } = new List<EnterpriseCategory>();
        public virtual ICollection<DocCategory> AllowedDocCategories { get; set; } = new List<DocCategory>();
    }
}
