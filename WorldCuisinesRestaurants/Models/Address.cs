using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Address
    {
            public int ID { get; set; }

            public string Street { get; set; }

            public string City { get; set; }

            public string ZipCode { get; set; }

            public int RestaurantID { get; set; }



    }
}