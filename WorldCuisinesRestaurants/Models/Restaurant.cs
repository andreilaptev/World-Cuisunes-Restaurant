using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Restaurant
    {
        public int ID { get; set; }

        public string RestaurantName { get; set; }

        public Address AddressID { get; set; }

        public Menu MenuID { get; set; }
        
        public RestaurantType CuisineType { get; set; }

        public RestaurantSet Set { get; set; }

       

    }

    public enum RestaurantType
    {
        Indian,
        American,
        European,
        Oriental
    }
}