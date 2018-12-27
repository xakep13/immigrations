namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHoverImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "HoverImage", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItems", "HoverImage");
        }
    }
}
