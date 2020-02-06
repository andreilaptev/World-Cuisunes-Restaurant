using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Table
    {
        public int ID { get; set; }

        public int TableNo { get; set; }

        public int RestaurantID { get; set; }
    }
}