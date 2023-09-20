using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coolntern.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<double> GPA { get; set; }
        public Nullable<int> TrainingPoint { get; set; }
        public string Major { get; set; }
    }
}