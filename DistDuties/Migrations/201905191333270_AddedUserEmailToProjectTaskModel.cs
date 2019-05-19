namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserEmailToProjectTaskModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "TeamMateEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "TeamMateEmail");
        }
    }
}
