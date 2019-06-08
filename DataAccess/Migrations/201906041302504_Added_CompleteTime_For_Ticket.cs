namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_CompleteTime_For_Ticket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "CompleteTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "CompleteTime");
        }
    }
}
