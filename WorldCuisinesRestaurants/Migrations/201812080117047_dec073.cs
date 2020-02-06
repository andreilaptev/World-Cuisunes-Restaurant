namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec073 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DishIngredient", "Measure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DishIngredient", "Measure", c => c.String());
        }
    }
}
