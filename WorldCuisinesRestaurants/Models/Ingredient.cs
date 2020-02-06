using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Ingredient
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public string Measure { get; set; }
    }
}