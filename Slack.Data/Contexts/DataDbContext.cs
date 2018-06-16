using Slack.Data.Entities;
using System.Data.Entity;

namespace Slack.Data.Contexts
{
    public class DataDbContext : Slack.Data.Contexts.DbContext
    {
        public DataDbContext() : base("SlackDataDB") { }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Subscribe> Subs { get; set; }

        public DbSet<Dialog> Dialogs { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dialog>()
                .HasMany(d => d.Messages)
                .WithRequired(d => d.Dialog)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public override DbContext GetContext()
        {
            return new DataDbContext();
        }
    }
}
