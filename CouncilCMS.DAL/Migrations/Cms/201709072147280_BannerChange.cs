namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BannerChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "ImageLg", c => c.String(unicode: false));
            AddColumn("dbo.Banners", "ImageMd", c => c.String(unicode: false));
            AddColumn("dbo.Banners", "ImageSm", c => c.String(unicode: false));
            AddColumn("dbo.Banners", "ImageXs", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "ImageXs");
            DropColumn("dbo.Banners", "ImageSm");
            DropColumn("dbo.Banners", "ImageMd");
            DropColumn("dbo.Banners", "ImageLg");
        }
    }
}
