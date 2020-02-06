using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Menu
    {
        public int ID { get; set; }

        public string Name { get; set; }

        //public int RestaurantId { get; set; }
       
        public List<Dish> Dishes { get; set; }
    }
}