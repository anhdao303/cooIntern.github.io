using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coolntern.ViewModel
{
    public class EditProfile
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Range(0, 10, ErrorMessage = "GPA have to between 0 and 10")]
        public double? GPA { get; set; }
        [Range(0, 100, ErrorMessage = "Training point have to between 0 and 10")]
        [Display(Name = "Training point")]
        public int? TrainingPoint { get; set; }
        public string Major { get; set; }
        public string Experience { get; set; }
        public string Details { get; set; }
        public string Avatar { get; set; }
    }
}