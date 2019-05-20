namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedPropertyFromTeamMate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TeamMate", "TicketID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamMate", "TicketID", c => c.Int(nullable: false));
        }
    }
}
