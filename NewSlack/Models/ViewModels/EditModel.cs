using Slack.Common.Attributes;
using Slack.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slack.Models.ViewModels
{
    public class EditModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required field.")]
        [OnlyLetters(ErrorMessage = "Enter your name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Required field.")]
        [OnlyLetters(ErrorMessage = "Enter your last name.")]
        public string LastName { get; set; }

        [OnlyLetters(ErrorMessage = "Enter your country.")]
        public string Country { get; set; }

        [OnlyLetters(ErrorMessage = "Enter your city.")]
        public string City { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string Image { get; set; }
    }
}