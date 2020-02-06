using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class DishRestaurant
    {
        public int ID { get; set; }

        public int RestaurantID { get; set; }
        public int DishID { get; set; }
    }
}