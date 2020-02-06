namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adgdgdhsh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredient", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingredient", "Price", c => c.Double(nullable: false));
        }
    }
}
