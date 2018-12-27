namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessionsDocFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Docs", "AgendaId", "SessionAgendas");
            DropForeignKey("Docs", "VoteId", "SessionVotes");
            DropIndex("Docs", new[] { "VoteId" });
            DropIndex("Docs", new[] { "AgendaId" });
            DropColumn("dbo.Docs", "VoteId");
            DropColumn("dbo.Docs", "AgendaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Docs", "AgendaId", c => c.Int());
            AddColumn("dbo.Docs", "VoteId", c => c.Int());
            CreateIndex("dbo.Docs", "AgendaId");
            CreateIndex("dbo.Docs", "VoteId");
            AddForeignKey("dbo.Docs", "VoteId", "dbo.SessionVotes", "Id");
            AddForeignKey("dbo.Docs", "AgendaId", "dbo.SessionAgendas", "Id");
        }
    }
}
