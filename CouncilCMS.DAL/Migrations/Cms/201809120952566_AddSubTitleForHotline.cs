namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubTitleForHotline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "HotlineSubTitleUk", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "HotlineSubTitleRu", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "HotlineSubTitleEn", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "HotlineSubTitleEn");
            DropColumn("dbo.CmsSettings", "HotlineSubTitleRu");
            DropColumn("dbo.CmsSettings", "HotlineSubTitleUk");
        }
    }
}
