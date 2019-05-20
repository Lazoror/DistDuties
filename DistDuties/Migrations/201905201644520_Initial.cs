namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatorEmail = c.String(),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.TeamMate",
                c => new
                    {
                        TeamMateID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        ProjectID = c.Int(nullable: false),
                        TaskID = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.TeamMateID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.ProjectTask",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false, maxLength: 60),
                        ProjectID = c.Int(nullable: false),
                        TeamMateID = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        TeamMateEmail = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.TeamMate", t => t.TeamMateID, cascadeDelete: true)
                .Index(t => t.TeamMateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTask", "TeamMateID", "dbo.TeamMate");
            DropForeignKey("dbo.TeamMate", "ProjectID", "dbo.Project");
            DropIndex("dbo.ProjectTask", new[] { "TeamMateID" });
            DropIndex("dbo.TeamMate", new[] { "ProjectID" });
            DropTable("dbo.ProjectTask");
            DropTable("dbo.TeamMate");
            DropTable("dbo.Project");
        }
    }
}
