namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusForTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "Status");
        }
    }
}
