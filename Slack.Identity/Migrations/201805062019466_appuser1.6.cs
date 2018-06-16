namespace Slack.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuser16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImagePath");
        }
    }
}
