using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coolntern.ViewModel
{
    public class ApplyJob
    {
        public string userId { get; set; }
        public int jobId { get; set; }
        public string UserName { get; set; }
        public double? GPA { get; set; }
        public int? TrainingPoint {get; set;}
        public string JobName { get; set;}
        public short? JobState { get; set;}
        public DateTime? DateApply { get; set;}
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        [AllowHtml]
        public string Message { get; set; }
        public string NameJob { get; set; }
        public string LocationJob { get; set; }
        public string NameCompany { get; set; }
        public int? Salary { get; set; }
        public string ImageJob { get; set; }
        public DateTime? Dateapply { get; set; }
        public DateTime? Datebegin { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }

    }
}