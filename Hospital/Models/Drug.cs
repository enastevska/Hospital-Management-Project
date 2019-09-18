using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Drug
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter drug's name")]

        public string Name { get; set; }
        [Display(Name="Generic Name")]
        public string GenericName { get; set; }
        [Required(ErrorMessage = "Please enter drug's producer")]
        public string Producer { get; set; }
        [Required(ErrorMessage = "Please enter the usage of the drug")]

        public string Usage { get; set; }
        [Required(ErrorMessage = "Please enter drug's indications")]

        public string Indications { get; set; }
        public virtual List<Therapy> Therapies { get; set; }

        public Drug()
        {
            Therapies = new List<Therapy>();
        }

    }
}