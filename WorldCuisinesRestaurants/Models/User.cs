using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorldCuisinesRestaurants.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public UserRole UserRole { get; set; }

        public Nullable<float> TotalAmountSpent { get; set; }
   
        public Nullable<int> MemberID { get; set; }

    }


    public enum UserRole
    {
        Member,
        Manager,
        Admin
    }
}