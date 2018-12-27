namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAgendaFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "AgendaFile", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "AgendaFile");
        }
    }
}
