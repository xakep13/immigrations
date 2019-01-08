using System.Data.Entity;
using System;
using Bissoft.CouncilCMS.DAL.Entities;
using Bissoft.CouncilCMS.Entities.Entities;

namespace Bissoft.CouncilCMS.DAL.Contexts
{
    public class CmsDbContext : BaseDbContext
    {
        public CmsDbContext() : base(false)
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public CmsDbContext(Boolean disableInitializer = true) : base(disableInitializer)
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public CmsDbContext(String connectionString, Boolean disableInitializer = true) : base(connectionString, disableInitializer)
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Doc> Docs { get; set; }
        public DbSet<DocCategory> DocCategories { get; set; }
        public DbSet<DocCategoryTemplate> DocCategoryTemplates { get; set; }

		public DbSet<Page> Pages { get; set; }
        public DbSet<PageTemplate> PageTemplates { get; set; }

		public DbSet<DamagedHousing> DamagedHousings { get; set; }
		public DbSet<DamagedHousingCategory> DamagedHousingCategories { get; set; }
		public DbSet<DamagedHousingCategoryTemplate> DamagedHousingCategoryTemplates { get; set; }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<EnterpriseCategory> EnterpriseCategories { get; set; }
        public DbSet<EnterpriseCategoryTemplate> EnterpriseCategoryTemplates { get; set; }
        public DbSet<EnterpriseEnterpriseCategory> EnterpriseEnterpriseCategories { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleCategoryTemplate> ArticleCategoryTemplates { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonCategory> PersonCategories { get; set; }
        public DbSet<PersonPersonCategory> PersonPersonCategories { get; set; }
        public DbSet<PersonCategoryTemplate> PersonCategoryTemplates { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentRow> ContentRows { get; set; }
        public DbSet<ContentColumn> ContentColumns { get; set; }

        public DbSet<ContentWidget> ContentWidgets { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionVote> SessionVotes { get; set; }
        public DbSet<SessionAgenda> SessionAgendas { get; set; }

        public DbSet<CmsSearch> CmsSearches { get; set; }
        public DbSet<CmsSettings> CmsSettings { get; set; }

        public DbSet<CmsUser> CmsUsers { get; set; }
        public DbSet<CmsRole> CmsRoles { get; set; }
        public DbSet<CmsPremission> CmsPremissions { get; set; }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }

        //------------------------------------

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Persons");

            modelBuilder.Entity<CmsUser>()
                .HasMany(x => x.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("CmsUserCmsRoles");
                    m.MapLeftKey("CmsUser_Id"); m.MapRightKey("CmsRole_Id");
                });

            modelBuilder.Entity<MenuItem>()
                .HasMany(x => x.AllowedRoles)
                .WithMany(r => r.AllowedMenuItems)
                .Map(m => { m.ToTable("MenuItemCmsRoles"); m.MapLeftKey("MenuItem_Id"); m.MapRightKey("CmsRole_Id"); });
            modelBuilder.Entity<ArticleCategory>()
                .HasMany(x => x.AllowedRoles)
                .WithMany(r => r.AllowedArticleCategories)
                .Map(m => { m.ToTable("ArticleCategoryCmsRoles"); m.MapLeftKey("ArticleCategory_Id"); m.MapRightKey("CmsRole_Id"); });
            modelBuilder.Entity<PersonCategory>()
                .HasMany(x => x.AllowedRoles)
                .WithMany(r => r.AllowedPersonCategories)
                .Map(m => { m.ToTable("PersonCategoryCmsRoles"); m.MapLeftKey("PersonCategory_Id"); m.MapRightKey("CmsRole_Id"); });
            modelBuilder.Entity<EnterpriseCategory>()
                .HasMany(x => x.AllowedRoles)
                .WithMany(r => r.AllowedEnterpriseCategories)
                .Map(m => { m.ToTable("EnterpriseCategoryCmsRoles"); m.MapLeftKey("EnterpriseCategory_Id"); m.MapRightKey("CmsRole_Id"); });
            modelBuilder.Entity<DocCategory>()
                .HasMany(x => x.AllowedRoles)
                .WithMany(r => r.AllowedDocCategories)
                .Map(m => { m.ToTable("DocCategoryCmsRoles"); m.MapLeftKey("DocCategory_Id"); m.MapRightKey("CmsRole_Id"); });

            modelBuilder.Entity<ArticleCategory>()
               .HasMany(x => x.AllowedUsers)
               .WithMany(r => r.AllowedArticleCategories)
               .Map(m => { m.ToTable("ArticleCategoryCmsUsers"); m.MapLeftKey("ArticleCategory_Id"); m.MapRightKey("CmsUser_Id"); });
            modelBuilder.Entity<PersonCategory>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(r => r.AllowedPersonCategories)
                .Map(m => { m.ToTable("PersonCategoryCmsUsers"); m.MapLeftKey("PersonCategory_Id"); m.MapRightKey("CmsUser_Id"); });
            modelBuilder.Entity<EnterpriseCategory>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(r => r.AllowedEnterpriseCategories)
                .Map(m => { m.ToTable("EnterpriseCategoryCmsUsers"); m.MapLeftKey("EnterpriseCategory_Id"); m.MapRightKey("CmsUser_Id"); });
            modelBuilder.Entity<DocCategory>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(r => r.AllowedDocCategories)
                .Map(m => { m.ToTable("DocCategoryCmsUsers"); m.MapLeftKey("DocCategory_Id"); m.MapRightKey("CmsUser_Id"); });

            modelBuilder.Entity<Page>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(r => r.AllowedPages)
                .Map(m => { m.ToTable("PageCmsUsers"); m.MapLeftKey("Page_Id"); m.MapRightKey("CmsUser_Id"); });
            modelBuilder.Entity<Person>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(r => r.AllowedPersons)
                .Map(m => { m.ToTable("PersonCmsUsers"); m.MapLeftKey("Person_Id"); m.MapRightKey("CmsUser_Id"); });
            modelBuilder.Entity<Enterprise>()
                .HasMany(x => x.AllowedUsers)
                .WithMany(r => r.AllowedEnterprises)
                .Map(m => { m.ToTable("EnterpriseCmsUsers"); m.MapLeftKey("Enterprise_Id"); m.MapRightKey("CmsUser_Id"); });

            modelBuilder.Entity<Article>()
               .HasMany(x => x.Categories)
               .WithMany(r => r.Articles)
               .Map(m => { m.ToTable("ArticleArticleCategories"); m.MapLeftKey("Article_Id"); m.MapRightKey("ArticleCategory_Id"); });
            modelBuilder.Entity<Doc>()
               .HasMany(x => x.Categories)
               .WithMany(r => r.Docs)
               .Map(m => { m.ToTable("DocDocCategories"); m.MapLeftKey("Doc_Id"); m.MapRightKey("DocCategory_Id"); });

            modelBuilder.Entity<Person>()
                .HasMany(x => x.Categories).WithRequired(p => p.Person).HasForeignKey(k => k.PersonId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Enterprise>()
                .HasMany(x => x.Categories).WithRequired(p => p.Enterprise).HasForeignKey(k => k.EnterpriseId).WillCascadeOnDelete(true);
            modelBuilder.Entity<PersonCategory>()
                .HasMany(x => x.Persons).WithRequired(p => p.Category).HasForeignKey(k => k.CategoryId).WillCascadeOnDelete(true);
            modelBuilder.Entity<EnterpriseCategory>()
                .HasMany(x => x.Enterprises).WithRequired(p => p.Category).HasForeignKey(k => k.CategoryId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Page>()
                .HasOptional(x => x.CreateUser).WithMany(x => x.CreatedPages).HasForeignKey(k => k.CreateUserId);
            modelBuilder.Entity<Page>()
                .HasOptional(x => x.LastEditUser).WithMany(x => x.LastEditedPages).HasForeignKey(k => k.LastEditUserId);
            modelBuilder.Entity<Article>()
               .HasOptional(x => x.CreateUser).WithMany(x => x.CreatedArticles).HasForeignKey(k => k.CreateUserId);
            modelBuilder.Entity<Article>()
                .HasOptional(x => x.LastEditUser).WithMany(x => x.LastEditedArticles).HasForeignKey(k => k.LastEditUserId);
            modelBuilder.Entity<Person>()
               .HasOptional(x => x.CreateUser).WithMany(x => x.CreatedPersons).HasForeignKey(k => k.CreateUserId);
            modelBuilder.Entity<Person>()
                .HasOptional(x => x.LastEditUser).WithMany(x => x.LastEditedPersons).HasForeignKey(k => k.LastEditUserId);
            modelBuilder.Entity<Enterprise>()
               .HasOptional(x => x.CreateUser).WithMany(x => x.CreatedEnterprises).HasForeignKey(k => k.CreateUserId);
            modelBuilder.Entity<Enterprise>()
                .HasOptional(x => x.LastEditUser).WithMany(x => x.LastEditedEnterprises).HasForeignKey(k => k.LastEditUserId);
            modelBuilder.Entity<Doc>()
                .HasOptional(x => x.CreateUser).WithMany(x => x.CreatedDocs).HasForeignKey(k => k.CreateUserId);
            modelBuilder.Entity<Doc>()
                .HasOptional(x => x.LastEditUser).WithMany(x => x.LastEditedDocs).HasForeignKey(k => k.LastEditUserId);
            modelBuilder.Entity<Session>()
               .HasOptional(x => x.CreateUser).WithMany(x => x.CreatedSessions).HasForeignKey(k => k.CreateUserId);
            modelBuilder.Entity<Session>()
                .HasOptional(x => x.LastEditUser).WithMany(x => x.LastEditedSessions).HasForeignKey(k => k.LastEditUserId);

            modelBuilder.Entity<MenuItem>()
               .HasRequired<Menu>(d => d.Menu)
               .WithMany(d => d.MenuItems)
               .HasForeignKey(x => x.MenuId)
               .WillCascadeOnDelete(true);

			modelBuilder.Entity<MenuItem>()
                .HasOptional<MenuItem>(d => d.ParentMenuItem)
                .WithMany(d => d.ChildMenuItems)
                .HasForeignKey(x => x.ParentMenuItemId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ArticleCategory>()
                .HasOptional<ArticleCategory>(d => d.ParentCategory)
                .WithMany(d => d.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId);

            modelBuilder.Entity<ArticleCategory>()
               .HasOptional<ArticleCategory>(d => d.RelatedCategory)
               .WithMany(d => d.RelatedCategories)
               .HasForeignKey(x => x.RelatedCategoryId);

			modelBuilder.Entity<DamagedHousingCategory>()
			   .HasOptional<DamagedHousingCategory>(d => d.ParentCategory)
			   .WithMany(d => d.ChildCategories)
			   .HasForeignKey(x => x.ParentCategoryId);

			modelBuilder.Entity<DamagedHousingCategory>()
			   .HasOptional<DamagedHousingCategory>(d => d.RelatedCategory)
			   .WithMany(d => d.RelatedCategories)
			   .HasForeignKey(x => x.RelatedCategoryId);

			modelBuilder.Entity<PersonCategory>()
                .HasOptional<PersonCategory>(d => d.ParentCategory)
                .WithMany(d => d.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId);

            modelBuilder.Entity<PersonCategory>()
                .HasOptional<PersonCategory>(d => d.RelatedCategory)
                .WithMany(d => d.RelatedCategories)
                .HasForeignKey(x => x.RelatedCategoryId);

            modelBuilder.Entity<EnterpriseCategory>()
                .HasOptional<EnterpriseCategory>(d => d.ParentCategory)
                .WithMany(d => d.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId);

            modelBuilder.Entity<EnterpriseCategory>()
               .HasOptional<EnterpriseCategory>(d => d.RelatedCategory)
               .WithMany(d => d.RelatedCategories)
               .HasForeignKey(x => x.RelatedCategoryId);

            modelBuilder.Entity<DocCategory>()
                .HasOptional<DocCategory>(d => d.ParentCategory)
                .WithMany(d => d.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId);

            modelBuilder.Entity<DocCategory>()
               .HasOptional<DocCategory>(d => d.RelatedCategory)
               .WithMany(d => d.RelatedCategories)
               .HasForeignKey(x => x.RelatedCategoryId);

            modelBuilder.Entity<SessionAgenda>()
                .HasOptional<Doc>(d => d.Doc)
                .WithMany(d => d.SessionAgendas)
                .HasForeignKey(x => x.DocId);

            modelBuilder.Entity<SessionVote>()
                .HasOptional<Doc>(d => d.Doc)
                .WithMany(d => d.SessionVotes)
                .HasForeignKey(x => x.DocId);

            modelBuilder.Entity<Session>()
               .HasMany<Doc>(d => d.Projects)
               .WithMany(d => d.Sessions);

            modelBuilder.Entity<Page>()
                .HasMany(a => a.ContentRows)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("PageId");
                    x.MapRightKey("ContentRowId");
                    x.ToTable("PageContentRow");
                });

            modelBuilder.Entity<Article>()
                .HasMany(a => a.ContentRows)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("ArticleId");
                    x.MapRightKey("ContenRowId");
                    x.ToTable("ArticleContentRow");
                });

			

			modelBuilder.Entity<Person>()
                .HasMany(a => a.ContentRows)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("PersonId");
                    x.MapRightKey("ContenRowId");
                    x.ToTable("PersonContentRow");
                });

            modelBuilder.Entity<Enterprise>()
                .HasMany(a => a.ContentRows)
                .WithMany()
                 .Map(x =>
                 {
                     x.MapLeftKey("EnterpriseId");
                     x.MapRightKey("ContenRowId");
                     x.ToTable("EnterpriseContentRow");
                 });
        }
    }
}
