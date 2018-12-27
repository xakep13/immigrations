namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorForQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Color", c => c.String(unicode: false));
            AlterColumn("dbo.Polls", "DateFrom", c => c.String(unicode: false));
            AlterColumn("dbo.Polls", "DateTo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Polls", "DateTo", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Polls", "DateFrom", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Questions", "Color");
        }
    }
}
