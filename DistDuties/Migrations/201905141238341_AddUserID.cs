namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TeamMates");
            AddColumn("dbo.TeamMates", "UserID", c => c.String());
            AlterColumn("dbo.TeamMates", "TeamMateID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TeamMates", "TeamMateID");
            DropColumn("dbo.TeamMates", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamMates", "Email", c => c.String());
            DropPrimaryKey("dbo.TeamMates");
            AlterColumn("dbo.TeamMates", "TeamMateID", c => c.Int(nullable: false));
            DropColumn("dbo.TeamMates", "UserID");
            AddPrimaryKey("dbo.TeamMates", "TeamMateID");
        }
    }
}
