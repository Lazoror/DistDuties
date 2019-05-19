namespace DistDuties.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMateEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamMates", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamMates", "Email");
        }
    }
}
