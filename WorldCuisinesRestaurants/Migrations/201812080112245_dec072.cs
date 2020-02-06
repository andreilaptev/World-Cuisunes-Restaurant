namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec072 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        RestaurantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cuisine",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Percentage = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dish",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Cost = c.Single(nullable: false),
                        Menu_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Menu", t => t.Menu_ID)
                .Index(t => t.Menu_ID);
            
            CreateTable(
                "dbo.DishIngredient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dish_ID = c.Int(nullable: false),
                        Ingredient_ID = c.Int(nullable: false),
                        Measure = c.String(),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DishRestaurant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RestaurantID = c.Int(nullable: false),
                        DishID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Measure = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        UserRole = c.Int(nullable: false),
                        TotalAmountSpent = c.Single(),
                        MemberID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuDIsh",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Menu_ID = c.Int(nullable: false),
                        Dish_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDish",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Order_ID = c.Int(nullable: false),
                        Dish_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        TotalSum = c.Double(nullable: false),
                        Restaurant_ID = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Promotion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Restaurant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        CuisineType = c.Int(nullable: false),
                        AddressID_ID = c.Int(),
                        MenuID_ID = c.Int(),
                        Set_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.AddressID_ID)
                .ForeignKey("dbo.Menu", t => t.MenuID_ID)
                .ForeignKey("dbo.RestaurantSet", t => t.Set_ID)
                .Index(t => t.AddressID_ID)
                .Index(t => t.MenuID_ID)
                .Index(t => t.Set_ID);
            
            CreateTable(
                "dbo.RestaurantSet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOfTables = c.Int(nullable: false),
                        HtmlSnippet = c.String(),
                        PicturePath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoginName = c.String(),
                        Password = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurant", "Set_ID", "dbo.RestaurantSet");
            DropForeignKey("dbo.Restaurant", "MenuID_ID", "dbo.Menu");
            DropForeignKey("dbo.Restaurant", "AddressID_ID", "dbo.Address");
            DropForeignKey("dbo.Order", "User_ID", "dbo.User");
            DropForeignKey("dbo.Dish", "Menu_ID", "dbo.Menu");
            DropIndex("dbo.Restaurant", new[] { "Set_ID" });
            DropIndex("dbo.Restaurant", new[] { "MenuID_ID" });
            DropIndex("dbo.Restaurant", new[] { "AddressID_ID" });
            DropIndex("dbo.Order", new[] { "User_ID" });
            DropIndex("dbo.Dish", new[] { "Menu_ID" });
            DropTable("dbo.UserLogin");
            DropTable("dbo.RestaurantSet");
            DropTable("dbo.Restaurant");
            DropTable("dbo.Promotion");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDish");
            DropTable("dbo.Menu");
            DropTable("dbo.MenuDIsh");
            DropTable("dbo.User");
            DropTable("dbo.Ingredient");
            DropTable("dbo.DishRestaurant");
            DropTable("dbo.DishIngredient");
            DropTable("dbo.Dish");
            DropTable("dbo.Discount");
            DropTable("dbo.Cuisine");
            DropTable("dbo.Address");
        }
    }
}
