namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Drugs", "Therapy_Id", "dbo.Therapies");
            DropIndex("dbo.Drugs", new[] { "Therapy_Id" });
            CreateTable(
                "dbo.DrugTherapies",
                c => new
                    {
                        Drug_Id = c.Int(nullable: false),
                        Therapy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Drug_Id, t.Therapy_Id })
                .ForeignKey("dbo.Drugs", t => t.Drug_Id, cascadeDelete: true)
                .ForeignKey("dbo.Therapies", t => t.Therapy_Id, cascadeDelete: true)
                .Index(t => t.Drug_Id)
                .Index(t => t.Therapy_Id);
            
            AlterColumn("dbo.Appointments", "ToTime", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Therapies", "DateFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Therapies", "DateTo", c => c.DateTime(nullable: false));
            DropColumn("dbo.Drugs", "Therapy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drugs", "Therapy_Id", c => c.Int());
            DropForeignKey("dbo.DrugTherapies", "Therapy_Id", "dbo.Therapies");
            DropForeignKey("dbo.DrugTherapies", "Drug_Id", "dbo.Drugs");
            DropIndex("dbo.DrugTherapies", new[] { "Therapy_Id" });
            DropIndex("dbo.DrugTherapies", new[] { "Drug_Id" });
            AlterColumn("dbo.Therapies", "DateTo", c => c.String(nullable: false));
            AlterColumn("dbo.Therapies", "DateFrom", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "DateOfBirth", c => c.String(nullable: false));
            AlterColumn("dbo.Appointments", "ToTime", c => c.String());
            DropTable("dbo.DrugTherapies");
            CreateIndex("dbo.Drugs", "Therapy_Id");
            AddForeignKey("dbo.Drugs", "Therapy_Id", "dbo.Therapies", "Id");
        }
    }
}
