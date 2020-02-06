using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class DishIngredient
    {
        public int ID { get; set; }

        public int Dish_ID { get; set; }

        public int Ingredient_ID { get; set; }

        public double Quantity { get; set; }

    }
}