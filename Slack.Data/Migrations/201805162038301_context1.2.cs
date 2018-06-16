namespace Slack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class context12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dialogs", "HiddenUsers", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dialogs", "HiddenUsers");
        }
    }
}
