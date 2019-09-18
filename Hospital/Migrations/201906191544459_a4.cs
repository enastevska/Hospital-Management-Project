namespace Hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Therapy2", "Drug_Id", "dbo.Drugs");
            DropIndex("dbo.Therapy2", new[] { "Drug_Id" });
            AddColumn("dbo.Therapy2", "DrugId", c => c.Int(nullable: false));
            DropColumn("dbo.Therapy2", "Drug_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Therapy2", "Drug_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Therapy2", "DrugId");
            CreateIndex("dbo.Therapy2", "Drug_Id");
            AddForeignKey("dbo.Therapy2", "Drug_Id", "dbo.Drugs", "Id", cascadeDelete: true);
        }
    }
}
