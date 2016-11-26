namespace WEPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update261120163 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" }, "dbo.Restaurants");
            DropPrimaryKey("dbo.Restaurants");
            AlterColumn("dbo.Restaurants", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Restaurants", new[] { "Id", "Name" });
            AddForeignKey("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" }, "dbo.Restaurants", new[] { "Id", "Name" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" }, "dbo.Restaurants");
            DropPrimaryKey("dbo.Restaurants");
            AlterColumn("dbo.Restaurants", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Restaurants", new[] { "Id", "Name" });
            AddForeignKey("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" }, "dbo.Restaurants", new[] { "Id", "Name" }, cascadeDelete: true);
        }
    }
}
