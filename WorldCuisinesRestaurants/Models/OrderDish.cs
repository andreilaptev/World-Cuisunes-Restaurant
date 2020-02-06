using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class OrderDish
    {
        public int ID { get; set; }

        public int Order_ID { get; set; }

        public int Dish_ID { get; set; }
    }
}