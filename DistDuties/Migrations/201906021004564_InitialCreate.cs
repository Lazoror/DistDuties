namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectID = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatorEmail = c.String(),
                        ProjectStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.TeamMate",
                c => new
                    {
                        TeamMateID = c.Guid(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        ProjectID = c.Guid(nullable: false),
                        Email = c.String(),
                        MateStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamMateID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketID = c.Guid(nullable: false, identity: true),
                        TicketName = c.String(nullable: false, maxLength: 60),
                        ProjectID = c.Guid(nullable: false),
                        TeamMateID = c.Guid(nullable: false),
                        Description = c.String(nullable: false),
                        TeamMateEmail = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.TeamMate", t => t.TeamMateID, cascadeDelete: true)
                .Index(t => t.TeamMateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "TeamMateID", "dbo.TeamMate");
            DropForeignKey("dbo.TeamMate", "ProjectID", "dbo.Project");
            DropIndex("dbo.Ticket", new[] { "TeamMateID" });
            DropIndex("dbo.TeamMate", new[] { "ProjectID" });
            DropTable("dbo.Ticket");
            DropTable("dbo.TeamMate");
            DropTable("dbo.Project");
        }
    }
}
