namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "FromTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Appointments", "ToTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "ToTime", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "FromTime", c => c.String(nullable: false));
        }
    }
}
