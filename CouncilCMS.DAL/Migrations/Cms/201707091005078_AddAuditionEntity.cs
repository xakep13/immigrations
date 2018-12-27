namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditionEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enterprises", "LastEditDate", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enterprises", "LastEditDate", c => c.DateTime(nullable: false, precision: 0));
        }
    }
}
