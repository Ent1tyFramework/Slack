using Slack.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Data.Interfaces
{
    public interface IRepositoryManager
    {
        IGenericRepository<T> Set<T>() where T : class;
    }
}
