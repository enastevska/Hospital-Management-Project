using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the date of the appointment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select a patient")]
        [Display(Name ="Patient")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Please select a doctor")]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }


        [Required(ErrorMessage = "Please enter starting time of the appointment")]
        [Display(Name = "From time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public string FromTime { get; set; }


        [Display(Name = "Patient")]
        public Patient Patient { get; set; }
        [Display(Name = "Doctor")]
        public Doctor Doctor { get; set; }

       [Display(Name ="Has Occured")]
        public bool HasOccured { get; set; }



    }
}