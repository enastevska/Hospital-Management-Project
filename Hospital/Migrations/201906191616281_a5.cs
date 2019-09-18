namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DrugTherapies", "Drug_Id", "dbo.Drugs");
            DropForeignKey("dbo.DrugTherapies", "Therapy_Id", "dbo.Therapies");
            DropIndex("dbo.DrugTherapies", new[] { "Drug_Id" });
            DropIndex("dbo.DrugTherapies", new[] { "Therapy_Id" });
            AddColumn("dbo.Therapies", "DrugId", c => c.Int(nullable: false));
            CreateIndex("dbo.Therapies", "DrugId");
            AddForeignKey("dbo.Therapies", "DrugId", "dbo.Drugs", "Id", cascadeDelete: true);
            DropTable("dbo.DrugTherapies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DrugTherapies",
                c => new
                    {
                        Drug_Id = c.Int(nullable: false),
                        Therapy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Drug_Id, t.Therapy_Id });
            
            DropForeignKey("dbo.Therapies", "DrugId", "dbo.Drugs");
            DropIndex("dbo.Therapies", new[] { "DrugId" });
            DropColumn("dbo.Therapies", "DrugId");
            CreateIndex("dbo.DrugTherapies", "Therapy_Id");
            CreateIndex("dbo.DrugTherapies", "Drug_Id");
            AddForeignKey("dbo.DrugTherapies", "Therapy_Id", "dbo.Therapies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DrugTherapies", "Drug_Id", "dbo.Drugs", "Id", cascadeDelete: true);
        }
    }
}
