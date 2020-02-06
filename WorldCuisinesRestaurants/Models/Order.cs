using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Order
    {
        public  int ID { get; set; }

        public int User_ID { get; set; }

        public DateTime Date { get; set; }

        public double TotalSum { get; set; }

        public int Restaurant_ID { get; set; }

        public int OrderNumber { get; set; }
    }
}