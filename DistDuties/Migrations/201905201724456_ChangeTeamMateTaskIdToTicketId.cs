namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTeamMateTaskIdToTicketId : DbMigration
    {
        public override void Up()
        {
            RenameColumn("TeamMate", "TaskID", "TicketID");
            
        }
        
        public override void Down()
        {
            RenameColumn("TeamMate", "TicketID", "TaskID");
        }
    }
}
