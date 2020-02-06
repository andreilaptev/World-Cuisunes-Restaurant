namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec14_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Table",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TableNo = c.Int(nullable: false),
                        RestaurantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Table");
        }
    }
}
