using Slack.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Data.Entities
{
    public class Dialog
    {
        public int Id { get; set; }

        [NotNull]
        public string Users { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public bool Private { get; set; }

        [NotNull]
        public string HiddenUsers { get; set; }
    }
}
