namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemCmsRoles",
                c => new
                    {
                        MenuItem_Id = c.Int(nullable: false),
                        CmsRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuItem_Id, t.CmsRole_Id })
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsRoles", t => t.CmsRole_Id, cascadeDelete: true)
                .Index(t => t.MenuItem_Id)
                .Index(t => t.CmsRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItemCmsRoles", "CmsRole_Id", "dbo.CmsRoles");
            DropForeignKey("dbo.MenuItemCmsRoles", "MenuItem_Id", "dbo.MenuItems");
            DropIndex("dbo.MenuItemCmsRoles", new[] { "CmsRole_Id" });
            DropIndex("dbo.MenuItemCmsRoles", new[] { "MenuItem_Id" });
            DropTable("dbo.MenuItemCmsRoles");
        }
    }
}
