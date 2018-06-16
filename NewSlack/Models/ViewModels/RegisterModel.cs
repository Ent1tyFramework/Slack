using Slack.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Slack.Common.Enums;



namespace Slack.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Required field.")]
        [EmailAddress(ErrorMessage = "Please, enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [DataType(DataType.Password, ErrorMessage = "Insecure password.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password length must be at least 8 and not more 30 chars.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Required field.")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password, ErrorMessage = "Insecure password.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required field.")]
        [OnlyLetters(ErrorMessage = "Enter your name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Required field.")]
        [OnlyLetters(ErrorMessage = "Enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field.")]
        public Gender Gender { get; set; }

        [OnlyLetters(ErrorMessage = "Enter your country.")]
        public string Country { get; set; }

        [OnlyLetters(ErrorMessage = "Enter your city.")]
        public string City { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public DateTime? RegDate { get; set; }
    }
}