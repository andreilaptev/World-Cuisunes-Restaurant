using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class RestaurantSet
    {
        public int ID { get; set; }

        public int NumberOfTables { get; set; }

        public string HtmlSnippet { get; set; }

        public string PicturePath { get; set; }

        public int RestaurantID { get; set; }



    }
}