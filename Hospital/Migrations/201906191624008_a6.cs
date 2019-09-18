namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Therapy2", "PatientId", "dbo.Patients");
            DropIndex("dbo.Therapy2", new[] { "PatientId" });
            DropTable("dbo.Therapy2");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Therapy2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrugId = c.Int(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        Diagnose = c.String(nullable: false),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Therapy2", "PatientId");
            AddForeignKey("dbo.Therapy2", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
