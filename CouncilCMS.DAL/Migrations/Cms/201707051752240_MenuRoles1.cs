namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuRoles1 : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "CmsRoleArticleCategories", newName: "ArticleCategoryCmsRoles");
            //RenameTable(name: "CmsUserArticleCategories", newName: "ArticleCategoryCmsUsers");
            //RenameTable(name: "CmsUserDocCategories", newName: "DocCategoryCmsUsers");
            //DropPrimaryKey("dbo.ArticleCategoryCmsRoles");
            //DropPrimaryKey("dbo.ArticleCategoryCmsUsers");
            //DropPrimaryKey("dbo.DocCategoryCmsUsers");
            //AddPrimaryKey("dbo.ArticleCategoryCmsRoles", new[] { "ArticleCategory_Id", "CmsRole_Id" });
            //AddPrimaryKey("dbo.ArticleCategoryCmsUsers", new[] { "ArticleCategory_Id", "CmsUser_Id" });
            //AddPrimaryKey("dbo.DocCategoryCmsUsers", new[] { "DocCategory_Id", "CmsUser_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DocCategoryCmsUsers");
            DropPrimaryKey("dbo.ArticleCategoryCmsUsers");
            DropPrimaryKey("dbo.ArticleCategoryCmsRoles");
            AddPrimaryKey("dbo.DocCategoryCmsUsers", new[] { "CmsUser_Id", "DocCategory_Id" });
            AddPrimaryKey("dbo.ArticleCategoryCmsUsers", new[] { "CmsUser_Id", "ArticleCategory_Id" });
            AddPrimaryKey("dbo.ArticleCategoryCmsRoles", new[] { "CmsRole_Id", "ArticleCategory_Id" });
            RenameTable(name: "dbo.DocCategoryCmsUsers", newName: "CmsUserDocCategories");
            RenameTable(name: "dbo.ArticleCategoryCmsUsers", newName: "CmsUserArticleCategories");
            RenameTable(name: "dbo.ArticleCategoryCmsRoles", newName: "CmsRoleArticleCategories");
        }
    }
}
