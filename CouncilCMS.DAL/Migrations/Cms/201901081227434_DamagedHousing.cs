namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DamagedHousing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DamagedHousingCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AllowOpenDataExport = c.Boolean(nullable: false),
                        UrlName = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Image = c.String(unicode: false),
                        ShowSearchForm = c.Boolean(nullable: false),
                        TemplateId = c.Int(),
                        SidebarMenuId = c.Int(),
                        CurrentMenuItemId = c.Int(),
                        RelatedCategoryId = c.Int(),
                        ParentCategoryId = c.Int(),
                        Deleted = c.Boolean(nullable: false),
                        DamagedHousing_Id = c.Int(),
                        DamagedHousing_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.CurrentMenuItemId)
                .ForeignKey("dbo.DamagedHousings", t => t.DamagedHousing_Id)
                .ForeignKey("dbo.DamagedHousings", t => t.DamagedHousing_Id1)
                .ForeignKey("dbo.DamagedHousingCategories", t => t.ParentCategoryId)
                .ForeignKey("dbo.DamagedHousingCategories", t => t.RelatedCategoryId)
                .ForeignKey("dbo.Menus", t => t.SidebarMenuId)
                .ForeignKey("dbo.DamagedHousingCategoryTemplates", t => t.TemplateId)
                .Index(t => t.TemplateId)
                .Index(t => t.SidebarMenuId)
                .Index(t => t.CurrentMenuItemId)
                .Index(t => t.RelatedCategoryId)
                .Index(t => t.ParentCategoryId)
                .Index(t => t.DamagedHousing_Id)
                .Index(t => t.DamagedHousing_Id1);
            
            CreateTable(
                "dbo.DamagedHousings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(unicode: false),
                        Adress = c.String(unicode: false),
                        FullName = c.String(unicode: false),
                        UrlNameRu = c.String(unicode: false),
                        UrlNameUk = c.String(unicode: false),
                        UrlNameEn = c.String(unicode: false),
                        DescriptionUk = c.String(unicode: false),
                        DescriptionRu = c.String(unicode: false),
                        DescriptionEn = c.String(unicode: false),
                        KeywordsUk = c.String(unicode: false),
                        KeywordsRu = c.String(unicode: false),
                        KeywordsEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        ShowPublihDate = c.Boolean(nullable: false),
                        ShowEditDate = c.Boolean(nullable: false),
                        AllowComments = c.Boolean(nullable: false),
                        EventDate = c.DateTime(precision: 0),
                        EditedDate = c.DateTime(precision: 0),
                        Saved = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        ImageSource = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        PublishDate = c.DateTime(precision: 0),
                        LastEditDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(),
                        LastEditUserId = c.Int(),
                        Published = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DamagedHousingCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CmsUsers", t => t.CreateUserId)
                .ForeignKey("dbo.CmsUsers", t => t.LastEditUserId)
                .ForeignKey("dbo.DamagedHousingCategories", t => t.DamagedHousingCategory_Id)
                .Index(t => t.CreateUserId)
                .Index(t => t.LastEditUserId)
                .Index(t => t.DamagedHousingCategory_Id);
            
            CreateTable(
                "dbo.DamagedHousingCategoryTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Preview = c.String(unicode: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Position = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CmsRoles", "DamagedHousingCategory_Id", c => c.Int());
            AddColumn("dbo.CmsUsers", "DamagedHousingCategory_Id", c => c.Int());
            AddColumn("dbo.ContentRows", "DamagedHousing_Id", c => c.Int());
            CreateIndex("dbo.CmsRoles", "DamagedHousingCategory_Id");
            CreateIndex("dbo.CmsUsers", "DamagedHousingCategory_Id");
            CreateIndex("dbo.ContentRows", "DamagedHousing_Id");
            AddForeignKey("dbo.CmsRoles", "DamagedHousingCategory_Id", "dbo.DamagedHousingCategories", "Id");
            AddForeignKey("dbo.CmsUsers", "DamagedHousingCategory_Id", "dbo.DamagedHousingCategories", "Id");
            AddForeignKey("dbo.ContentRows", "DamagedHousing_Id", "dbo.DamagedHousings", "Id");
            
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.DamagedHousingCategories", "TemplateId", "dbo.DamagedHousingCategoryTemplates");
            DropForeignKey("dbo.DamagedHousingCategories", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.DamagedHousingCategories", "RelatedCategoryId", "dbo.DamagedHousingCategories");
            DropForeignKey("dbo.DamagedHousingCategories", "ParentCategoryId", "dbo.DamagedHousingCategories");
            DropForeignKey("dbo.DamagedHousings", "DamagedHousingCategory_Id", "dbo.DamagedHousingCategories");
            DropForeignKey("dbo.DamagedHousingCategories", "DamagedHousing_Id1", "dbo.DamagedHousings");
            DropForeignKey("dbo.DamagedHousings", "LastEditUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.DamagedHousings", "CreateUserId", "dbo.CmsUsers");
            DropForeignKey("dbo.ContentRows", "DamagedHousing_Id", "dbo.DamagedHousings");
            DropForeignKey("dbo.DamagedHousingCategories", "DamagedHousing_Id", "dbo.DamagedHousings");
            DropForeignKey("dbo.DamagedHousingCategories", "CurrentMenuItemId", "dbo.MenuItems");
            DropForeignKey("dbo.CmsUsers", "DamagedHousingCategory_Id", "dbo.DamagedHousingCategories");
            DropForeignKey("dbo.CmsRoles", "DamagedHousingCategory_Id", "dbo.DamagedHousingCategories");
            DropIndex("dbo.DamagedHousings", new[] { "DamagedHousingCategory_Id" });
            DropIndex("dbo.DamagedHousings", new[] { "LastEditUserId" });
            DropIndex("dbo.DamagedHousings", new[] { "CreateUserId" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "DamagedHousing_Id1" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "DamagedHousing_Id" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "ParentCategoryId" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "RelatedCategoryId" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "CurrentMenuItemId" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "SidebarMenuId" });
            DropIndex("dbo.DamagedHousingCategories", new[] { "TemplateId" });
            DropIndex("dbo.ContentRows", new[] { "DamagedHousing_Id" });
            DropIndex("dbo.CmsUsers", new[] { "DamagedHousingCategory_Id" });
            DropIndex("dbo.CmsRoles", new[] { "DamagedHousingCategory_Id" });
            DropColumn("dbo.ContentRows", "DamagedHousing_Id");
            DropColumn("dbo.CmsUsers", "DamagedHousingCategory_Id");
            DropColumn("dbo.CmsRoles", "DamagedHousingCategory_Id");
            DropTable("dbo.DamagedHousingCategoryTemplates");
            DropTable("dbo.DamagedHousings");
            DropTable("dbo.DamagedHousingCategories");
        }
    }
}
