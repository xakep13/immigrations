namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPollAndQuestions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        DateFrom = c.DateTime(precision: 0),
                        DateTo = c.DateTime(precision: 0),
                        VoiсeCount = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        CheckCount = c.Int(nullable: false),
                        PollID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Polls", t => t.PollID)
                .Index(t => t.PollID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "PollID", "dbo.Polls");
            DropIndex("dbo.Questions", new[] { "PollID" });
            DropTable("dbo.Questions");
            DropTable("dbo.Polls");
        }
    }
}
