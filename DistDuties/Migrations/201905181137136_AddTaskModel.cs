namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        ProjectID = c.Int(nullable: false),
                        Description = c.String(),
                        TeamMate_TeamMateID = c.Int(),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.TeamMates", t => t.TeamMate_TeamMateID)
                .Index(t => t.TeamMate_TeamMateID);
            
            AddColumn("dbo.TeamMates", "TaskID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "TeamMate_TeamMateID", "dbo.TeamMates");
            DropIndex("dbo.ProjectTasks", new[] { "TeamMate_TeamMateID" });
            DropColumn("dbo.TeamMates", "TaskID");
            DropTable("dbo.ProjectTasks");
        }
    }
}
