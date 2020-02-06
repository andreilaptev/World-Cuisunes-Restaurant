namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantSet", "RestaurantID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantSet", "RestaurantID");
        }
    }
}
