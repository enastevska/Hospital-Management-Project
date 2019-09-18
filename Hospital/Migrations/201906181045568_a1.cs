namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Therapies", "Diagnose", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Therapies", "Diagnose", c => c.String());
        }
    }
}
