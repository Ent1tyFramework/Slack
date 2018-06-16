using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Data.Attributes
{
    public class NotNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!String.IsNullOrEmpty(value.ToString()))
                return true;
            else return false;
        }
    }
}
