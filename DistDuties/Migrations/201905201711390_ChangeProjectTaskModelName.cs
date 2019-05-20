namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProjectTaskModelName : DbMigration
    {
        public override void Up()
        {
            RenameTable("ProjectTask", "Ticket");
            RenameColumn("Ticket", "TaskID", "TicketId");
        }

        public override void Down()
        {
            RenameColumn("Ticket", "TicketId", "TaskID");
            RenameTable("Ticket", "ProjectTask");
        }
    }
}
