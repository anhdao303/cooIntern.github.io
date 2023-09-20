using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coolntern.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be blank")]
        [MinLength(6, ErrorMessage = "Password at least 6 characters ")]
        public string Password { get; set; }
    }
}