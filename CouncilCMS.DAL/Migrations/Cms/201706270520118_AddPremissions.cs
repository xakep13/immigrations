namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPremissions : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CmsPremissions",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TitleUk = c.String(unicode: false),
            //            TitleRu = c.String(unicode: false),
            //            TitleEn = c.String(unicode: false),
            //            Name = c.String(unicode: false),
            //            Position = c.Int(nullable: false),
            //            Deleted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.CmsRoles", "TitleUk", c => c.String(unicode: false));
            //AddColumn("dbo.CmsRoles", "TitleRu", c => c.String(unicode: false));
            //AddColumn("dbo.CmsRoles", "TitleEn", c => c.String(unicode: false));
            //DropColumn("dbo.CmsRoles", "Title");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.CmsRoles", "Title", c => c.String(unicode: false));
            //DropColumn("dbo.CmsRoles", "TitleEn");
            //DropColumn("dbo.CmsRoles", "TitleRu");
            //DropColumn("dbo.CmsRoles", "TitleUk");
            //DropTable("dbo.CmsPremissions");
        }
    }
}
