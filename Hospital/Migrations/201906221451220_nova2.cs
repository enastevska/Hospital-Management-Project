namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nova2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "FromTime", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "FromTime");
        }
    }
}
