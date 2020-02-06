namespace WorldCuisinesRestaurants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dec10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "User_ID", "dbo.User");
            DropIndex("dbo.Order", new[] { "User_ID" });
            AlterColumn("dbo.Order", "User_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "User_ID", c => c.Int());
            CreateIndex("dbo.Order", "User_ID");
            AddForeignKey("dbo.Order", "User_ID", "dbo.User", "ID");
        }
    }
}
