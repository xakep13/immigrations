namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserStat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "CreateUserId", c => c.Int());
            AddColumn("dbo.Sessions", "LastEditUserId", c => c.Int());
            CreateIndex("dbo.Enterprises", "CreateUserId");
            CreateIndex("dbo.Enterprises", "LastEditUserId");
            CreateIndex("dbo.Docs", "CreateUserId");
            CreateIndex("dbo.Docs", "LastEditUserId");
            CreateIndex("dbo.Persons", "CreateUserId");
            CreateIndex("dbo.Persons", "LastEditUserId");
            CreateIndex("dbo.Sessions", "CreateUserId");
            CreateIndex("dbo.Sessions", "LastEditUserId");
            CreateIndex("dbo.Pages", "CreateUserId");
            CreateIndex("dbo.Pages", "LastEditUserId");
            CreateIndex("dbo.Articles", "CreateUserId");
            CreateIndex("dbo.Articles", "LastEditUserId");
            AddForeignKey("dbo.Enterprises", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Docs", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Docs", "LastEditUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Persons", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Persons", "LastEditUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Sessions", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Sessions", "LastEditUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Enterprises", "LastEditUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Pages", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Pages", "LastEditUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Articles", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Articles", "LastEditUserId", "dbo.CmsUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Articles", "CreateUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Pages", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Pages", "CreateUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Enterprises", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Sessions", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Sessions", "CreateUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Persons", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Persons", "CreateUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Docs", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Docs", "CreateUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Enterprises", "CreateUserId", "dbo.CmsUsers");
            DropIndex("dbo.Articles", new[] { "LastEditUserId" });
            DropIndex("dbo.Articles", new[] { "CreateUserId" });
            DropIndex("dbo.Pages", new[] { "LastEditUserId" });
            DropIndex("dbo.Pages", new[] { "CreateUserId" });
            DropIndex("dbo.Sessions", new[] { "LastEditUserId" });
            DropIndex("dbo.Sessions", new[] { "CreateUserId" });
            DropIndex("dbo.Persons", new[] { "LastEditUserId" });
            DropIndex("dbo.Persons", new[] { "CreateUserId" });
            DropIndex("dbo.Docs", new[] { "LastEditUserId" });
            DropIndex("dbo.Docs", new[] { "CreateUserId" });
            DropIndex("dbo.Enterprises", new[] { "LastEditUserId" });
            DropIndex("dbo.Enterprises", new[] { "CreateUserId" });
            DropColumn("dbo.Sessions", "LastEditUserId");
            DropColumn("dbo.Sessions", "CreateUserId");
        }
    }
}
