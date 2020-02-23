namespace FoodBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        ProdId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prods", t => t.ProdId, cascadeDelete: true)
                .Index(t => t.ProdId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zords", "ProdId", "dbo.Prods");
            DropIndex("dbo.Zords", new[] { "ProdId" });
            DropTable("dbo.Zords");
            DropTable("dbo.Prods");
        }
    }
}
