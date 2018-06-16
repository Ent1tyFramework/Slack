using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Slack.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string ImagePath { get; set; }

    }
}
