namespace ITIMVCProjectV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.Double(nullable: false),
                        Salary = c.Double(nullable: false),
                        Image = c.String(),
                        Amount = c.Int(nullable: false),
                        TotleAmount = c.Int(nullable: false),
                        ProviderName = c.String(),
                        Category_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_id, cascadeDelete: true)
                .Index(t => t.Category_id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Rate = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                        Customer_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResevationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DeliveryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cost = c.Double(nullable: false),
                        destination = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        Customer_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_id, cascadeDelete: true)
                .Index(t => t.Customer_id);
            
            CreateTable(
                "dbo.SubOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Order_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.Order_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .Index(t => t.Order_id)
                .Index(t => t.Product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Feedbacks", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.SubOrders", "Product_id", "dbo.Products");
            DropForeignKey("dbo.SubOrders", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_id", "dbo.Customers");
            DropForeignKey("dbo.Products", "Category_id", "dbo.Categories");
            DropIndex("dbo.SubOrders", new[] { "Product_id" });
            DropIndex("dbo.SubOrders", new[] { "Order_id" });
            DropIndex("dbo.Orders", new[] { "Customer_id" });
            DropIndex("dbo.Feedbacks", new[] { "Customer_ID" });
            DropIndex("dbo.Feedbacks", new[] { "Product_ID" });
            DropIndex("dbo.Products", new[] { "Category_id" });
            DropTable("dbo.SubOrders");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Admins");
        }
    }
}
