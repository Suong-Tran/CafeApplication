namespace CafeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favitemid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "ItemID");
            AddForeignKey("dbo.Customers", "ItemID", "dbo.Items", "ItemID");
            DropColumn("dbo.Customers", "favoriteItem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "favoriteItem", c => c.String());
            DropForeignKey("dbo.Customers", "ItemID", "dbo.Items");
            DropIndex("dbo.Customers", new[] { "ItemID" });
            DropColumn("dbo.Customers", "ItemID");
        }
    }
}
