namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSettings3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "SubLogoUk", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "SubLogoRu", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "SubLogoEn", c => c.String(unicode: false));
            AddColumn("dbo.CmsSettings", "Type", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "Type");
            DropColumn("dbo.CmsSettings", "SubLogoEn");
            DropColumn("dbo.CmsSettings", "SubLogoRu");
            DropColumn("dbo.CmsSettings", "SubLogoUk");
        }
    }
}
