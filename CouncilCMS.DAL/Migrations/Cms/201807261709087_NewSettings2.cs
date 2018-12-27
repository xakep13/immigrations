namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewSettings2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "HotlineColor", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "HotlineColor");
        }
    }
}
