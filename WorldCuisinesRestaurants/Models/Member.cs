using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class Member : User
    {
        public int MemberID { get; set; }

       public float TotalAmountSpent { get; set; }
    }
}