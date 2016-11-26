namespace WEPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update26112016 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Logs", new[] { "Restaurant_Id" });
            DropTable("dbo.Logs");
        }
    }
}
