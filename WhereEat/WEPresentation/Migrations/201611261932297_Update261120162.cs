namespace WEPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update261120162 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants");
            DropIndex("dbo.Logs", new[] { "Restaurant_Id" });
            DropPrimaryKey("dbo.Restaurants");
            AddColumn("dbo.Logs", "Restaurant_Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Restaurants", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Restaurants", new[] { "Id", "Name" });
            CreateIndex("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" });
            AddForeignKey("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" }, "dbo.Restaurants", new[] { "Id", "Name" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" }, "dbo.Restaurants");
            DropIndex("dbo.Logs", new[] { "Restaurant_Id", "Restaurant_Name" });
            DropPrimaryKey("dbo.Restaurants");
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Logs", "Restaurant_Name");
            AddPrimaryKey("dbo.Restaurants", "Id");
            CreateIndex("dbo.Logs", "Restaurant_Id");
            AddForeignKey("dbo.Logs", "Restaurant_Id", "dbo.Restaurants", "Id", cascadeDelete: true);
        }
    }
}
