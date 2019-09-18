namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a61 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "ToTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "ToTime", c => c.DateTime(nullable: false));
        }
    }
}
