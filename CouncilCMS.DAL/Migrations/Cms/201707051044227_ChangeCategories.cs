namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCategories : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.EnterpriseDocs", newName: "DocEnterprises");

            //DropForeignKey("EnterpriseEnterpriseCategories", "EnterpriseCategoryId", "EnterpriseCategories");
            //DropForeignKey("PersonPersonCategories", "PersonCategoryId", "PersonCategories");

            //DropForeignKey("Enterprises", "EnterpriseCategory_Id", "EnterpriseCategories");
            //DropIndex("Enterprises", new[] { "EnterpriseCategory_Id" });
            

            //DropIndex(table: "ArticleCategories", name: "IX_RelatedCategory_Id");

            //DropIndex(table: "ArticleCategories", name: "IX_ParentArticleCategoryId");
            //DropIndex(table: "PersonCategories", name: "IX_ParentPersonCategoryId");

            //DropIndex(table: "PersonPersonCategories", name: "IX_PersonCategoryId");
            //DropIndex(table: "EnterpriseEnterpriseCategories", name: "IX_EnterpriseCategoryId");

            //RenameColumn(table: "ArticleCategories", name: "RelatedCategory_Id", newName: "RelatedCategoryId");
            
            //RenameColumn(table: "EnterpriseEnterpriseCategories", name: "EnterpriseCategoryId", newName: "CategoryId");            
            //RenameColumn(table: "PersonPersonCategories", name: "PersonCategoryId", newName: "CategoryId");

            //RenameColumn(table: "ArticleCategories", name: "ParentArticleCategoryId", newName: "ParentCategoryId");
            //RenameColumn(table: "PersonCategories", name: "ParentPersonCategoryId", newName: "ParentCategoryId");

            //CreateIndex(table: "ArticleCategories", name: "IX_RelatedCategoryId", column: "RelatedCategoryId");

            //CreateIndex(table: "ArticleCategories", name: "IX_ParentCategoryId", column: "ParentCategoryId");
            //CreateIndex(table: "PersonCategories", name: "IX_ParentCategoryId", column: "ParentCategoryId");

            //CreateIndex(table: "PersonPersonCategories", name: "IX_CategoryId", column: "CategoryId");
            //CreateIndex(table: "EnterpriseEnterpriseCategories", name: "IX_CategoryId", column: "CategoryId");

            //DropPrimaryKey("dbo.DocEnterprises");

            CreateTable(
                "dbo.CmsRoleArticleCategories",
                c => new
                    {
                        CmsRole_Id = c.Int(nullable: false),
                        ArticleCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CmsRole_Id, t.ArticleCategory_Id })
                .ForeignKey("dbo.CmsRoles", t => t.CmsRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategory_Id, cascadeDelete: true)
                .Index(t => t.CmsRole_Id)
                .Index(t => t.ArticleCategory_Id);
            
            CreateTable(
                "dbo.CmsRoleDocCategories",
                c => new
                    {
                        CmsRole_Id = c.Int(nullable: false),
                        DocCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CmsRole_Id, t.DocCategory_Id })
                .ForeignKey("dbo.CmsRoles", t => t.CmsRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.DocCategories", t => t.DocCategory_Id, cascadeDelete: true)
                .Index(t => t.CmsRole_Id)
                .Index(t => t.DocCategory_Id);
            
            CreateTable(
                "dbo.EnterpriseCategoryCmsRoles",
                c => new
                    {
                        EnterpriseCategory_Id = c.Int(nullable: false),
                        CmsRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EnterpriseCategory_Id, t.CmsRole_Id })
                .ForeignKey("dbo.EnterpriseCategories", t => t.EnterpriseCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsRoles", t => t.CmsRole_Id, cascadeDelete: true)
                .Index(t => t.EnterpriseCategory_Id)
                .Index(t => t.CmsRole_Id);
            
            CreateTable(
                "dbo.PersonCategoryCmsRoles",
                c => new
                    {
                        PersonCategory_Id = c.Int(nullable: false),
                        CmsRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonCategory_Id, t.CmsRole_Id })
                .ForeignKey("dbo.PersonCategories", t => t.PersonCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.CmsRoles", t => t.CmsRole_Id, cascadeDelete: true)
                .Index(t => t.PersonCategory_Id)
                .Index(t => t.CmsRole_Id);
            
            AddColumn("dbo.DocCategories", "SidebarMenuId", c => c.Int());
            AddColumn("dbo.EnterpriseCategories", "SidebarMenuId", c => c.Int());
            AddColumn("dbo.EnterpriseCategories", "RelatedCategoryId", c => c.Int());
            AddColumn("dbo.PersonCategories", "RelatedCategoryId", c => c.Int());

            //AddPrimaryKey("dbo.DocEnterprises", new[] { "Doc_Id", "Enterprise_Id" });

            CreateIndex("dbo.DocCategories", "SidebarMenuId");
            CreateIndex("dbo.EnterpriseCategories", "SidebarMenuId");
            CreateIndex("dbo.EnterpriseCategories", "RelatedCategoryId");
            CreateIndex("dbo.PersonCategories", "RelatedCategoryId");

            AddForeignKey("dbo.PersonCategories", "RelatedCategoryId", "dbo.PersonCategories", "Id");
            AddForeignKey("dbo.EnterpriseCategories", "RelatedCategoryId", "dbo.EnterpriseCategories", "Id");
            AddForeignKey("dbo.EnterpriseCategories", "SidebarMenuId", "dbo.Menus", "Id");
            AddForeignKey("dbo.DocCategories", "SidebarMenuId", "dbo.Menus", "Id");

            //AddForeignKey("dbo.EnterpriseEnterpriseCategories", "CategoryId", "dbo.EnterpriseCategories", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.PersonPersonCategories", "CategoryId", "dbo.PersonCategories", "Id", cascadeDelete: true);

            DropColumn("dbo.ArticleCategories", "DefaultNoImage");
            DropColumn("dbo.ArticleCategories", "RelatedCategotyId");
            DropColumn("dbo.Enterprises", "EnterpriseCategory_Id");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnterpriseEnterpriseCategories", "EnterpriseCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Enterprises", "EnterpriseCategory_Id", c => c.Int());
            AddColumn("dbo.ArticleCategories", "RelatedCategotyId", c => c.Int());
            AddColumn("dbo.ArticleCategories", "DefaultNoImage", c => c.String(unicode: false));
            DropForeignKey("dbo.EnterpriseEnterpriseCategories", "CategoryId", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.DocCategories", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.EnterpriseCategories", "SidebarMenuId", "dbo.Menus");
            DropForeignKey("dbo.EnterpriseCategories", "RelatedCategoryId", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.PersonCategories", "RelatedCategoryId", "dbo.PersonCategories");
            DropForeignKey("dbo.PersonCategoryCmsRoles", "CmsRole_Id", "dbo.CmsRoles");
            DropForeignKey("dbo.PersonCategoryCmsRoles", "PersonCategory_Id", "dbo.PersonCategories");
            DropForeignKey("dbo.EnterpriseCategoryCmsRoles", "CmsRole_Id", "dbo.CmsRoles");
            DropForeignKey("dbo.EnterpriseCategoryCmsRoles", "EnterpriseCategory_Id", "dbo.EnterpriseCategories");
            DropForeignKey("dbo.CmsRoleDocCategories", "DocCategory_Id", "dbo.DocCategories");
            DropForeignKey("dbo.CmsRoleDocCategories", "CmsRole_Id", "dbo.CmsRoles");
            DropForeignKey("dbo.CmsRoleArticleCategories", "ArticleCategory_Id", "dbo.ArticleCategories");
            DropForeignKey("dbo.CmsRoleArticleCategories", "CmsRole_Id", "dbo.CmsRoles");
            DropIndex("dbo.PersonCategoryCmsRoles", new[] { "CmsRole_Id" });
            DropIndex("dbo.PersonCategoryCmsRoles", new[] { "PersonCategory_Id" });
            DropIndex("dbo.EnterpriseCategoryCmsRoles", new[] { "CmsRole_Id" });
            DropIndex("dbo.EnterpriseCategoryCmsRoles", new[] { "EnterpriseCategory_Id" });
            DropIndex("dbo.CmsRoleDocCategories", new[] { "DocCategory_Id" });
            DropIndex("dbo.CmsRoleDocCategories", new[] { "CmsRole_Id" });
            DropIndex("dbo.CmsRoleArticleCategories", new[] { "ArticleCategory_Id" });
            DropIndex("dbo.CmsRoleArticleCategories", new[] { "CmsRole_Id" });
            DropIndex("dbo.PersonCategories", new[] { "RelatedCategoryId" });
            DropIndex("dbo.EnterpriseEnterpriseCategories", new[] { "CategoryId" });
            DropIndex("dbo.EnterpriseCategories", new[] { "RelatedCategoryId" });
            DropIndex("dbo.EnterpriseCategories", new[] { "SidebarMenuId" });
            DropIndex("dbo.DocCategories", new[] { "SidebarMenuId" });
            DropPrimaryKey("dbo.DocEnterprises");
            DropColumn("dbo.PersonCategories", "RelatedCategoryId");
            DropColumn("dbo.EnterpriseCategories", "RelatedCategoryId");
            DropColumn("dbo.EnterpriseCategories", "SidebarMenuId");
            DropColumn("dbo.DocCategories", "SidebarMenuId");
            DropTable("dbo.PersonCategoryCmsRoles");
            DropTable("dbo.EnterpriseCategoryCmsRoles");
            DropTable("dbo.CmsRoleDocCategories");
            DropTable("dbo.CmsRoleArticleCategories");
            AddPrimaryKey("dbo.DocEnterprises", new[] { "Enterprise_Id", "Doc_Id" });
            RenameIndex(table: "dbo.PersonCategories", name: "IX_ParentCategoryId", newName: "IX_ParentPersonCategoryId");
            RenameIndex(table: "dbo.PersonPersonCategories", name: "IX_CategoryId", newName: "IX_PersonCategoryId");
            RenameIndex(table: "dbo.ArticleCategories", name: "IX_ParentCategoryId", newName: "IX_ParentArticleCategoryId");
            RenameIndex(table: "dbo.ArticleCategories", name: "IX_RelatedCategoryId", newName: "IX_RelatedCategory_Id");
            RenameColumn(table: "dbo.ArticleCategories", name: "ParentCategoryId", newName: "ParentArticleCategoryId");
            RenameColumn(table: "dbo.PersonPersonCategories", name: "CategoryId", newName: "PersonCategoryId");
            RenameColumn(table: "dbo.PersonCategories", name: "ParentCategoryId", newName: "ParentPersonCategoryId");
            RenameColumn(table: "dbo.EnterpriseEnterpriseCategories", name: "CategoryId", newName: "EnterpriseCategory_Id");
            RenameColumn(table: "dbo.ArticleCategories", name: "RelatedCategoryId", newName: "RelatedCategory_Id");
            CreateIndex("dbo.EnterpriseEnterpriseCategories", "EnterpriseCategoryId");
            CreateIndex("dbo.Enterprises", "EnterpriseCategory_Id");
            AddForeignKey("dbo.Enterprises", "EnterpriseCategory_Id", "dbo.EnterpriseCategories", "Id");
            AddForeignKey("dbo.EnterpriseEnterpriseCategories", "EnterpriseCategoryId", "dbo.EnterpriseCategories", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.DocEnterprises", newName: "EnterpriseDocs");
        }
    }
}
