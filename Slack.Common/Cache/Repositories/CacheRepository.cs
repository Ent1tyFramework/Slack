using Slack.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Slack.Common.Cache
{
    public class CacheRepository : ICacheRepository
    {
        private MemoryCache memoryCache;

        public CacheRepository()
        {
            memoryCache = MemoryCache.Default;
        }

        public object First(string key)
            => memoryCache.Get(key);

        public bool Add(object value, string key)
        {
            //if (First(key) != null)
            //    Delete(key);
            return memoryCache.Add(key, value, DateTime.Now.AddMinutes(10));
        }

        public void Update(object value, string key)
        {
            memoryCache.Set(key, value, DateTime.Now.AddMinutes(10));
        }

        public int Count()
            => memoryCache.Count();

        public object Delete(string key)
        {
            return memoryCache.Remove(key);
        }
    }
}
