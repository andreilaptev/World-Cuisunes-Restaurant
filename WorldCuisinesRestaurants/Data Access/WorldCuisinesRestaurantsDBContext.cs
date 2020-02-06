using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WorldCuisinesRestaurants.Models;

namespace WorldCuisinesRestaurants.Data_Access
{
    public class WorldCuisinesRestaurantsDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
       // public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantSet> RestaurantSets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dishes { get; set; }
 
        public DbSet<Member> Members { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<MenuDIsh> MenuDIshes { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.Cuisine> Cuisines { get; set; }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.Discount> Discounts { get; set; }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.Manager> Managers { get; set; }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.Promotion> Promotions { get; set; }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.DishRestaurant> DishRestaurants { get; set; }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.Ingredient> Ingredients { get; set; }

        public System.Data.Entity.DbSet<WorldCuisinesRestaurants.Models.Table> Tables { get; set; }
    }

   
}