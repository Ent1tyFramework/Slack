using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Common.Interfaces
{
    public interface ICacheRepository
    {
        object First(string key);
        bool Add(object value, string key);
        void Update(object value, string key);
        object Delete(string key);
        int Count();
    }
}
