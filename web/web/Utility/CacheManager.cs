using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Utility
{
    public class CacheManager
    {
        public void ClearAllCache()
        {
            var cache = HttpRuntime.Cache.GetEnumerator();
            while (cache.MoveNext())
            {
                HttpRuntime.Cache.Remove(cache.Key.ToString());
            }
        }

        public void ClearCacheByKey(string cacheCode)
        {
            if (HttpRuntime.Cache[cacheCode] != null)
                HttpRuntime.Cache.Remove(cacheCode);
        }

    }
}