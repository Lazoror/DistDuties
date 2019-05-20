namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTaskNameToTicketName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "TicketName", c => c.String(nullable: false, maxLength: 60));
            DropColumn("dbo.Ticket", "TaskName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ticket", "TaskName", c => c.String(nullable: false, maxLength: 60));
            DropColumn("dbo.Ticket", "TicketName");
        }
    }
}
