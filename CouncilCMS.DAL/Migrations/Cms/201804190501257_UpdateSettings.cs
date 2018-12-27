namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "SubTitleUk", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "SubTitleRu", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "SubTitleEn", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorCategoryId", c => c.Int());
            AddColumn("dbo.CmsSettings", "MayorTitleUk", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorTitleRu", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorTitleEn", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorImage", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorNameUk", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorNameRu", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "MayorNameEn", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "HotlinePhone", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "HotlineEmail", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "GoogleCaptchaApiSecret", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "FeedburnerName", c => c.String(unicode: false));
            CreateIndex("dbo.CmsSettings", "MayorCategoryId");
            AddForeignKey("dbo.CmsSettings", "MayorCategoryId", "dbo.ArticleCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CmsSettings", "MayorCategoryId", "dbo.ArticleCategories");
            DropIndex("dbo.CmsSettings", new[] { "MayorCategoryId" });
            DropColumn("dbo.CmsSettings", "FeedburnerName");
            DropColumn("dbo.CmsSettings", "GoogleCaptchaApiSecret");
            DropColumn("dbo.CmsSettings", "HotlineEmail");
            DropColumn("dbo.CmsSettings", "HotlinePhone");
            DropColumn("dbo.CmsSettings", "MayorNameEn");
            DropColumn("dbo.CmsSettings", "MayorNameRu");
            DropColumn("dbo.CmsSettings", "MayorNameUk");
            DropColumn("dbo.CmsSettings", "MayorImage");
            DropColumn("dbo.CmsSettings", "MayorTitleEn");
            DropColumn("dbo.CmsSettings", "MayorTitleRu");
            DropColumn("dbo.CmsSettings", "MayorTitleUk");
            DropColumn("dbo.CmsSettings", "MayorCategoryId");
            DropColumn("dbo.CmsSettings", "SubTitleEn");
            DropColumn("dbo.CmsSettings", "SubTitleRu");
            DropColumn("dbo.CmsSettings", "SubTitleUk");
        }
    }
}
