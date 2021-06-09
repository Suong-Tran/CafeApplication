namespace CafeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oderitem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "ItemName", c => c.String());
            CreateIndex("dbo.Orders", "CustimerId");
            AddForeignKey("dbo.Orders", "CustimerId", "dbo.Customers", "CustomerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustimerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustimerId" });
            AlterColumn("dbo.Items", "ItemName", c => c.Int(nullable: false));
        }
    }
}
