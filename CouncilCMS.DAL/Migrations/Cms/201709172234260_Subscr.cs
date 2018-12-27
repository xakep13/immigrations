namespace Bissoft.CouncilCMS.DAL.Migrations.Cms
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsletterEmailQueues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(unicode: false),
                        SentOn = c.DateTime(precision: 0),
                        NewsletterId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Newsletters", t => t.NewsletterId, cascadeDelete: true)
                .Index(t => t.NewsletterId);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(unicode: false),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsletterEmailQueues", "NewsletterId", "dbo.Newsletters");
            DropIndex("dbo.NewsletterEmailQueues", new[] { "NewsletterId" });
            DropTable("dbo.Subscribers");
            DropTable("dbo.NewsletterEmailQueues");
        }
    }
}
