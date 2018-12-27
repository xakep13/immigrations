namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonSecondPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persons", "ShowSecondPage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Persons", "SecondPageTitleUk", c => c.String(unicode: false));
            AddColumn("dbo.Persons", "SecondPageTitleRu", c => c.String(unicode: false));
            AddColumn("dbo.Persons", "SecondPageTitleEn", c => c.String(unicode: false));
            AddColumn("dbo.Persons", "SecondPageTextUk", c => c.String(unicode: false));
            AddColumn("dbo.Persons", "SecondPageTextRu", c => c.String(unicode: false));
            AddColumn("dbo.Persons", "SecondPageTextEn", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persons", "SecondPageTextEn");
            DropColumn("dbo.Persons", "SecondPageTextRu");
            DropColumn("dbo.Persons", "SecondPageTextUk");
            DropColumn("dbo.Persons", "SecondPageTitleEn");
            DropColumn("dbo.Persons", "SecondPageTitleRu");
            DropColumn("dbo.Persons", "SecondPageTitleUk");
            DropColumn("dbo.Persons", "ShowSecondPage");
        }
    }
}
