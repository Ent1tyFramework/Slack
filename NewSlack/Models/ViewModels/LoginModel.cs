using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slack.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Required field.")]
        [EmailAddress(ErrorMessage = "")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
    }
}