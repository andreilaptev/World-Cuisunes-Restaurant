namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec10_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderNumber");
        }
    }
}
