using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Discount
    {
        public int ID { get; set; }

        public int Percentage { get; set; }

        public int MemberID { get; set; }
    }
}