namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequiredTagsToProjectTaskModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProjectTasks", "TaskName", c => c.String(nullable: false));
            AlterColumn("dbo.ProjectTasks", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProjectTasks", "Description", c => c.String());
            AlterColumn("dbo.ProjectTasks", "TaskName", c => c.String());
        }
    }
}
