namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Therapy2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        Diagnose = c.String(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Drug_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drugs", t => t.Drug_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.Drug_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Therapy2", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Therapy2", "Drug_Id", "dbo.Drugs");
            DropIndex("dbo.Therapy2", new[] { "Drug_Id" });
            DropIndex("dbo.Therapy2", new[] { "PatientId" });
            DropTable("dbo.Therapy2");
        }
    }
}
