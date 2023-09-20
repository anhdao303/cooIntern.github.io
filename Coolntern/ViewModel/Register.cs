using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coolntern.ViewModel
{
    public class Register
    {
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        [MinLength(6, ErrorMessage = "Password at least 6 characters ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfrimPassword cannot be blank")]
        [Compare("Password", ErrorMessage ="Password and Confirm Password not match")]
        public string ConfrimPassword { get; set; }
        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "Email Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name cannot be blank")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}