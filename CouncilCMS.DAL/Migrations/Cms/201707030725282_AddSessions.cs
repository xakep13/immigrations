namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSessions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionAgendas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        Status = c.Int(nullable: false),
                        DocId = c.Int(),
                        SessionId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Docs", t => t.DocId)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.DocId)
                .Index(t => t.SessionId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        MetaTitleUk = c.String(unicode: false),
                        MetaTitleRu = c.String(unicode: false),
                        MetaTitleEn = c.String(unicode: false),
                        MetaDescriptionUk = c.String(unicode: false),
                        MetaDescriptionRu = c.String(unicode: false),
                        MetaDescriptionEn = c.String(unicode: false),
                        MetaKeywordsUk = c.String(unicode: false),
                        MetaKeywordsRu = c.String(unicode: false),
                        MetaKeywordsEn = c.String(unicode: false),
                        Ended = c.Boolean(nullable: false),
                        DecreeId = c.Int(),
                        ReglamentId = c.Int(),
                        AgendaAdditionId = c.Int(),
                        ProtocolId = c.Int(),
                        EmbedVideo = c.String(unicode: false),
                        EmbedAudio = c.String(unicode: false),
                        Published = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                        PublishDate = c.DateTime(precision: 0),
                        LastEditDate = c.DateTime(precision: 0),
                        SessionDate = c.DateTime(precision: 0),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Docs", t => t.AgendaAdditionId)
                .ForeignKey("dbo.Docs", t => t.DecreeId)
                .ForeignKey("dbo.Docs", t => t.ProtocolId)
                .ForeignKey("dbo.Docs", t => t.ReglamentId)
                .Index(t => t.DecreeId)
                .Index(t => t.ReglamentId)
                .Index(t => t.AgendaAdditionId)
                .Index(t => t.ProtocolId);
            
            CreateTable(
                "dbo.SessionVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        TitleRu = c.String(unicode: false),
                        TitleUk = c.String(unicode: false),
                        TitleEn = c.String(unicode: false),
                        TextRu = c.String(unicode: false),
                        TextUk = c.String(unicode: false),
                        TextEn = c.String(unicode: false),
                        ResultRu = c.String(unicode: false),
                        ResultUk = c.String(unicode: false),
                        ResultEn = c.String(unicode: false),
                        For = c.Int(nullable: false),
                        Against = c.Int(nullable: false),
                        Abstained = c.Int(nullable: false),
                        NotVote = c.Int(nullable: false),
                        Absent = c.Int(nullable: false),
                        Result = c.Int(nullable: false),
                        DocId = c.Int(),
                        SessionId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Docs", t => t.DocId)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.DocId)
                .Index(t => t.SessionId);
            
            CreateTable(
                "dbo.SessionDocs",
                c => new
                    {
                        Session_Id = c.Int(nullable: false),
                        Doc_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Session_Id, t.Doc_Id })
                .ForeignKey("dbo.Sessions", t => t.Session_Id, cascadeDelete: true)
                .ForeignKey("dbo.Docs", t => t.Doc_Id, cascadeDelete: true)
                .Index(t => t.Session_Id)
                .Index(t => t.Doc_Id);
            
            AddColumn("dbo.Docs", "VoteId", c => c.Int());
            AddColumn("dbo.Docs", "AgendaId", c => c.Int());
            AddColumn("dbo.CmsSettings", "FeedbackEmail", c => c.String(unicode: false));
            CreateIndex("dbo.Docs", "VoteId");
            CreateIndex("dbo.Docs", "AgendaId");
            AddForeignKey("dbo.Docs", "AgendaId", "dbo.SessionAgendas", "Id");
            AddForeignKey("dbo.Docs", "VoteId", "dbo.SessionVotes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Docs", "VoteId", "dbo.SessionVotes");
            DropForeignKey("dbo.Docs", "AgendaId", "dbo.SessionAgendas");
            DropForeignKey("dbo.SessionVotes", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.SessionVotes", "DocId", "dbo.Docs");
            DropForeignKey("dbo.Sessions", "ReglamentId", "dbo.Docs");
            DropForeignKey("dbo.Sessions", "ProtocolId", "dbo.Docs");
            DropForeignKey("dbo.SessionDocs", "Doc_Id", "dbo.Docs");
            DropForeignKey("dbo.SessionDocs", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.Sessions", "DecreeId", "dbo.Docs");
            DropForeignKey("dbo.Sessions", "AgendaAdditionId", "dbo.Docs");
            DropForeignKey("dbo.SessionAgendas", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.SessionAgendas", "DocId", "dbo.Docs");
            DropIndex("dbo.SessionDocs", new[] { "Doc_Id" });
            DropIndex("dbo.SessionDocs", new[] { "Session_Id" });
            DropIndex("dbo.SessionVotes", new[] { "SessionId" });
            DropIndex("dbo.SessionVotes", new[] { "DocId" });
            DropIndex("dbo.Sessions", new[] { "ProtocolId" });
            DropIndex("dbo.Sessions", new[] { "AgendaAdditionId" });
            DropIndex("dbo.Sessions", new[] { "ReglamentId" });
            DropIndex("dbo.Sessions", new[] { "DecreeId" });
            DropIndex("dbo.SessionAgendas", new[] { "SessionId" });
            DropIndex("dbo.SessionAgendas", new[] { "DocId" });
            DropIndex("dbo.Docs", new[] { "AgendaId" });
            DropIndex("dbo.Docs", new[] { "VoteId" });
            DropColumn("dbo.CmsSettings", "FeedbackEmail");
            DropColumn("dbo.Docs", "AgendaId");
            DropColumn("dbo.Docs", "VoteId");
            DropTable("dbo.SessionDocs");
            DropTable("dbo.SessionVotes");
            DropTable("dbo.Sessions");
            DropTable("dbo.SessionAgendas");
        }
    }
}
