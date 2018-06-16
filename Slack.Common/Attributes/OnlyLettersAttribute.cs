using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Common.Attributes
{
    public class OnlyLettersAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                foreach (char ch in value.ToString())
                {
                    if (!Char.IsLetter(ch)) return false;
                }
            }
            return true;
        }
    }
}
