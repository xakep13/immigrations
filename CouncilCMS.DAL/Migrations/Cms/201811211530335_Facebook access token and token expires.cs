namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Facebookaccesstokenandtokenexpires : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "FacebookUserAccessToken", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "FacebookUserAccessTokenExpires", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "FacebookUserAccessTokenExpires");
            DropColumn("dbo.CmsSettings", "FacebookUserAccessToken");
        }
    }
}
