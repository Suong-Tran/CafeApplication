namespace CafeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorderitem : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "CustimerId" });
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ItemID);
            
            CreateIndex("dbo.Orders", "CustimerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "ItemID", "dbo.Items");
            DropIndex("dbo.Orders", new[] { "CustimerID" });
            DropIndex("dbo.OrderItems", new[] { "ItemID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropTable("dbo.OrderItems");
            CreateIndex("dbo.Orders", "CustimerId");
        }
    }
}
