namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamMateEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamMates",
                c => new
                    {
                        TeamMateID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamMateID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamMates", "ProjectID", "dbo.Projects");
            DropIndex("dbo.TeamMates", new[] { "ProjectID" });
            DropTable("dbo.TeamMates");
        }
    }
}
