namespace CafeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingbridgingtable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "ItemID", "dbo.Items");
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropIndex("dbo.OrderItems", new[] { "ItemID" });
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Item_ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Item_ItemID })
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_ItemID, cascadeDelete: true)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Item_ItemID);
            
            AddColumn("dbo.Customers", "CustomerFName", c => c.String());
            AddColumn("dbo.Customers", "CustomerLName", c => c.String());
            DropColumn("dbo.Customers", "CustomerName");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.OrderItemID);
            
            AddColumn("dbo.Customers", "CustomerName", c => c.String());
            DropForeignKey("dbo.OrderItems", "Item_ItemID", "dbo.Items");
            DropForeignKey("dbo.OrderItems", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "Item_ItemID" });
            DropIndex("dbo.OrderItems", new[] { "Order_OrderID" });
            DropColumn("dbo.Customers", "CustomerLName");
            DropColumn("dbo.Customers", "CustomerFName");
            CreateIndex("dbo.OrderItems", "ItemID");
            CreateIndex("dbo.OrderItems", "OrderID");
            AddForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
            AddForeignKey("dbo.OrderItems", "ItemID", "dbo.Items", "ItemID", cascadeDelete: true);
        }
    }
}
