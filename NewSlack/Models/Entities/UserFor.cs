using Slack.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slack.Models.Entities
{
    //model was created for save user info(for dialogs, messages etc)
    public class UserFor
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
    }
}