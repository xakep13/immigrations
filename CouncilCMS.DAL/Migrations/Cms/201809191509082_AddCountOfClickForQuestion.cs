namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountOfClickForQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ClickCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "ClickCount");
        }
    }
}
