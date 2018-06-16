namespace Slack.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuser13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Login", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Login", c => c.Int(nullable: false));
        }
    }
}
