
namespace Slack.Data.Contexts
{
    public abstract class DbContext : System.Data.Entity.DbContext
    {
        public DbContext(string connectionString) : base(connectionString) { }

        public abstract DbContext GetContext();
    }
}
