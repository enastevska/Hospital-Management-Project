namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Therapies", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Therapies", new[] { "Patient_Id" });
            RenameColumn(table: "dbo.Therapies", name: "Patient_Id", newName: "PatientId");
            AddColumn("dbo.Therapies", "Diagnose", c => c.String());
            AlterColumn("dbo.Therapies", "PatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Therapies", "PatientId");
            AddForeignKey("dbo.Therapies", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Therapies", "PatientId", "dbo.Patients");
            DropIndex("dbo.Therapies", new[] { "PatientId" });
            AlterColumn("dbo.Therapies", "PatientId", c => c.Int());
            DropColumn("dbo.Therapies", "Diagnose");
            RenameColumn(table: "dbo.Therapies", name: "PatientId", newName: "Patient_Id");
            CreateIndex("dbo.Therapies", "Patient_Id");
            AddForeignKey("dbo.Therapies", "Patient_Id", "dbo.Patients", "Id");
        }
    }
}
