namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOldSiteField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "OldSite", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "OldSite");
        }
    }
}
