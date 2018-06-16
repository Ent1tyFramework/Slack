using Slack.Data.Attributes;

namespace Slack.Data.Entities
{
    public class Subscribe
    {
        public int Id { get; set; }

        [NotNull]
        public string UserFromId { get; set; }

        [NotNull]
        public string UserToId { get; set; }
    }
}
