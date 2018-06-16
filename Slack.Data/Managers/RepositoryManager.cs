using Slack.Data.Interfaces;
using Slack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace Slack.Data.Managers
{
    public class RepositoryManager : IRepositoryManager
    {
        private DbContext dbContext;

        public RepositoryManager(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IGenericRepository<T> Set<T>() where T : class
            => new GenericRepository<T>(dbContext);     
    }
}