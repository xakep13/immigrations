namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailAndPhoneStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CmsSettings", "IsEmailActive", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.CmsSettings", "IsPhoneActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CmsSettings", "IsEmailActive");
            DropColumn("dbo.CmsSettings", "IsPhoneActive");
        }
    }
}
