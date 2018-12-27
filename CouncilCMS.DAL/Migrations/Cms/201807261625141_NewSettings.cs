namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "HideMayor", c => c.Boolean(nullable: false));
            AddColumn("dbo.CmsSettings", "HotlineTitleUk", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "HotlineTitleRu", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "HotlineTitleEn", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "HotlineTitleEn");
            DropColumn("dbo.CmsSettings", "HotlineTitleRu");
            DropColumn("dbo.CmsSettings", "HotlineTitleUk");
            DropColumn("dbo.CmsSettings", "HideMayor");
        }
    }
}
