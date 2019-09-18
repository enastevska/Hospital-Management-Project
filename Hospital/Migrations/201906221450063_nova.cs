namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nova : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "FromTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "FromTime", c => c.String(nullable: false));
        }
    }
}
