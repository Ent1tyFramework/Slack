namespace Slack.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuser15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
