namespace CafeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingname : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "CustimerID", newName: "CustomerID");
            RenameIndex(table: "dbo.Orders", name: "IX_CustimerID", newName: "IX_CustomerID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_CustomerID", newName: "IX_CustimerID");
            RenameColumn(table: "dbo.Orders", name: "CustomerID", newName: "CustimerID");
        }
    }
}
