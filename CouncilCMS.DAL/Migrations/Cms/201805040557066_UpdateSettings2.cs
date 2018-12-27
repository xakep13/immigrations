namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSettings2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "FbLink", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "TwLink", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "YtLink", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "IgLink", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "IgLink");
            DropColumn("dbo.CmsSettings", "YtLink");
            DropColumn("dbo.CmsSettings", "TwLink");
            DropColumn("dbo.CmsSettings", "FbLink");
        }
    }
}
