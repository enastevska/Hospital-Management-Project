using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Therapy
    {
        public int Id { get; set; }
        [Required]
        public int DrugId { get; set; }

        [Required(ErrorMessage = "Please enter starting date of the therapy")]

        [Display(Name = "Date from")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFrom { get; set; }
        [Required(ErrorMessage = "Please enter therapy end date")]
        [Display(Name = "Date to")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTo { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Diagnose { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }


    }
}