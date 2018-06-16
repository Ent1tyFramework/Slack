using Slack.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace Slack.Data.Entities
{
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotNull]
        public string UserId { get; set; }

        [NotNull]
        public string Content { get; set; }

        public DateTime PublishTime { get; set; }
    }
}