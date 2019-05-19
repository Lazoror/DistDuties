namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTeamMateIdToProjectTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectTasks", "TeamMate_TeamMateID", "dbo.TeamMates");
            DropIndex("dbo.ProjectTasks", new[] { "TeamMate_TeamMateID" });
            RenameColumn(table: "dbo.ProjectTasks", name: "TeamMate_TeamMateID", newName: "TeamMateID");
            AlterColumn("dbo.ProjectTasks", "TeamMateID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectTasks", "TeamMateID");
            AddForeignKey("dbo.ProjectTasks", "TeamMateID", "dbo.TeamMates", "TeamMateID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "TeamMateID", "dbo.TeamMates");
            DropIndex("dbo.ProjectTasks", new[] { "TeamMateID" });
            AlterColumn("dbo.ProjectTasks", "TeamMateID", c => c.Int());
            RenameColumn(table: "dbo.ProjectTasks", name: "TeamMateID", newName: "TeamMate_TeamMateID");
            CreateIndex("dbo.ProjectTasks", "TeamMate_TeamMateID");
            AddForeignKey("dbo.ProjectTasks", "TeamMate_TeamMateID", "dbo.TeamMates", "TeamMateID");
        }
    }
}
