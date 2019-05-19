namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnsetAutoGenerationId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TeamMates");
            AlterColumn("dbo.TeamMates", "TeamMateID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TeamMates", "TeamMateID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TeamMates");
            AlterColumn("dbo.TeamMates", "TeamMateID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TeamMates", "TeamMateID");
        }
    }
}
