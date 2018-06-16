using Slack.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [NotNull]
        public string Content { get; set; }

        [NotNull]
        public Dialog Dialog { get; set; }

        [NotNull]
        public string UserId { get; set; }

        public DateTime PublishTime { get; set; }
    }
}
