using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Dish
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public float Cost { get; set; }

    }

}
