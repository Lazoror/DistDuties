namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLenghtFilterForTaskName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectTasks", "TaskName", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectTasks", "TaskName", c => c.String(nullable: false));
        }
    }
}
