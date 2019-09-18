namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Doctors", "Gender", c => c.Boolean(nullable: false));
        }
    }
}
