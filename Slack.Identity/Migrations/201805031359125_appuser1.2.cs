namespace Slack.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuser12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Login", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Login");
        }
    }
}
