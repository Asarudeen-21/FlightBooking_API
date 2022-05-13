using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string EmailID { get; set; }

        public long PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class Login
    {
        public string EmailID { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class LoginInformation
    {
        [Key]
        public int LoginID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public string EmailID { get; set; }

        public long PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
