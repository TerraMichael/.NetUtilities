namespace WEPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update261120161 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Logs", new[] { "Restaurant_Id" });
            AlterColumn("dbo.Logs", "Restaurant_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Logs", "Restaurant_Id");
            AddForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Logs", new[] { "Restaurant_Id" });
            AlterColumn("dbo.Logs", "Restaurant_Id", c => c.Int());
            CreateIndex("dbo.Logs", "Restaurant_Id");
            AddForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants", "Id");
        }
    }
}
