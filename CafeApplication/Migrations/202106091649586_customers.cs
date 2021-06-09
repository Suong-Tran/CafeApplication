namespace CafeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerGender = c.String(),
                        CustomerAge = c.Int(nullable: false),
                        isRegular = c.Boolean(nullable: false),
                        favoriteItem = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
