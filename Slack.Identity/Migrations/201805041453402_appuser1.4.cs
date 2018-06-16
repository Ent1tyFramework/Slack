namespace Slack.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuser14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Birthday");
        }
    }
}
