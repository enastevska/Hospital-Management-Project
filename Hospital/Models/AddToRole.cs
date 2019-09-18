using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class AddToRole
    {
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        public string selectedRole { get; set; }
        public List<string> roles { get; set; }
        public AddToRole()
        {
            roles = new List<string>();
        }
    }
}