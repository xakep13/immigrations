namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuListItemPosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Index");
        }
    }
}
