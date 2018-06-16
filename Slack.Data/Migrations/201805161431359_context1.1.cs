namespace Slack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class context11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Users = c.String(),
                        Name = c.String(),
                        Private = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        UserId = c.String(),
                        PublishTime = c.DateTime(nullable: false),
                        Dialog_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.Dialog_Id, cascadeDelete: true)
                .Index(t => t.Dialog_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Content = c.String(),
                        PublishTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserFromId = c.String(),
                        UserToId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Dialog_Id", "dbo.Dialogs");
            DropIndex("dbo.Messages", new[] { "Dialog_Id" });
            DropTable("dbo.Subscribes");
            DropTable("dbo.Posts");
            DropTable("dbo.Messages");
            DropTable("dbo.Dialogs");
        }
    }
}
