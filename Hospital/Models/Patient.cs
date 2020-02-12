using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Please enter patient's name and surname")]
        [Display(Name ="Name Surname")]
        public string NameSurName { get; set; }
      
        [Required(ErrorMessage = "Please enter patient's age")]
        [Range(0,150, ErrorMessage ="Please enter a valid age") ]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please select patient's gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter patient's date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name="Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please enter patient's address")]
     
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter patient's phone number")]
        [RegularExpression(@"(\+48\s\d{3}\s\d{3}\s\d{3})", ErrorMessage = "The phone number is not valid. Ex: +48 111 111 111")]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter patient's email")]
        [EmailAddress(ErrorMessage ="Please enter a valid email")]
        [Display(Name="E-mail")]
        public string Email { get; set; }
        [Display(Name = "Blood Type")]

        public string BloodType { get; set; }
        [DataType(DataType.MultilineText)]
        public string Allergies { get; set; }
        [Display(Name = "History of Diseases")]
        [DataType(DataType.MultilineText)]
        public string HistoryOfDiseases { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Therapy> Therapies { get; set; }

        public Patient()
        {
            this.Doctors = new List<Doctor>();
            Appointments = new List<Appointment>();
            Therapies = new List<Therapy>();
        }


       


    }
}