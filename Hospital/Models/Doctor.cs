using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter doctor's name and surname")]
        [Display(Name = "Name Surname")]
        public string NameSurName { get; set; }

        [Required(ErrorMessage = "Please enter doctor's age")]
        [Range(18,80,ErrorMessage ="Please enter a valid age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter doctor's gender")]

        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter doctor's phone number")]
        [RegularExpression(@"(\+48\s\d{3}\s\d{3}\s\d{3})", ErrorMessage = "The phone number is not valid. Ex: +48 111 111 111")]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter doctor's email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [Display(Name ="E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter doctor's specialty")]

        public string Specialty { get; set; }
        [Required(ErrorMessage = "Please enter the name of the Hospital ")]

        public string Hospital { get; set; }
        
        [Display(Name="Doctor's picture")]
        public string ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Please enter some more info about the doctor")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual List<Patient> Patients { get; set; }
        public List<Appointment> Appointments { get; set; }


        public Doctor()
        {
            this.Patients = new List<Patient>();
            Appointments = new List<Appointment>();
        }

   
  





    }
}