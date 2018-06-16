using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slack.Models.ViewModels
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Required field.")]
        [DataType(DataType.Password, ErrorMessage = "Insecure password.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password length must be at least 8 and not more 30 chars.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Required field.")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password, ErrorMessage = "Insecure password.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}