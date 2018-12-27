namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class facebookappkeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "FacebookAppId", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "FacebookSecretKey", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "FacebookPublicPageId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "FacebookPublicPageId");
            DropColumn("dbo.CmsSettings", "FacebookSecretKey");
            DropColumn("dbo.CmsSettings", "FacebookAppId");
        }
    }
}
