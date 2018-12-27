namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPollAndQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Polls", "TitleRu", c => c.String(unicode: false));
            AddColumn("dbo.Polls", "TitleUk", c => c.String(unicode: false));
            AddColumn("dbo.Polls", "TitleEn", c => c.String(unicode: false));
            AddColumn("dbo.Polls", "CreateDate", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.Polls", "PublishDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.Polls", "LastEditDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.Polls", "CreateUserId", c => c.Int());
            AddColumn("dbo.Polls", "LastEditUserId", c => c.Int());
            AddColumn("dbo.Polls", "Published", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "NameRu", c => c.String(unicode: false));
            AddColumn("dbo.Questions", "NameUk", c => c.String(unicode: false));
            AddColumn("dbo.Questions", "NameEn", c => c.String(unicode: false));
            AddColumn("dbo.Questions", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Polls", "DateFrom", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Polls", "DateTo", c => c.DateTime(nullable: false, precision: 0));
            CreateIndex("dbo.Polls", "CreateUserId");
            CreateIndex("dbo.Polls", "LastEditUserId");
            AddForeignKey("dbo.Polls", "CreateUserId", "dbo.CmsUsers", "Id");
            AddForeignKey("dbo.Polls", "LastEditUserId", "dbo.CmsUsers", "Id");
            DropColumn("dbo.Polls", "Title");
            DropColumn("dbo.Questions", "Title");
            DropColumn("dbo.Questions", "CheckCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "CheckCount", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Title", c => c.String(unicode: false));
            AddColumn("dbo.Polls", "Title", c => c.String(unicode: false));
            DropForeignKey("dbo.Polls", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.Polls", "CreateUserId", "dbo.CmsUsers");
            DropIndex("dbo.Polls", new[] { "LastEditUserId" });
            DropIndex("dbo.Polls", new[] { "CreateUserId" });
            AlterColumn("dbo.Polls", "DateTo", c => c.DateTime(precision: 0));
            AlterColumn("dbo.Polls", "DateFrom", c => c.DateTime(precision: 0));
            DropColumn("dbo.Questions", "Deleted");
            DropColumn("dbo.Questions", "Position");
            DropColumn("dbo.Questions", "NameEn");
            DropColumn("dbo.Questions", "NameUk");
            DropColumn("dbo.Questions", "NameRu");
            DropColumn("dbo.Polls", "Published");
            DropColumn("dbo.Polls", "LastEditUserId");
            DropColumn("dbo.Polls", "CreateUserId");
            DropColumn("dbo.Polls", "LastEditDate");
            DropColumn("dbo.Polls", "PublishDate");
            DropColumn("dbo.Polls", "CreateDate");
            DropColumn("dbo.Polls", "TitleEn");
            DropColumn("dbo.Polls", "TitleUk");
            DropColumn("dbo.Polls", "TitleRu");
        }
    }
}
