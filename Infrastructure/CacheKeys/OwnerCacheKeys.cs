using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheKeys
{
   public static class OwnerCacheKeys
    {
        public static string ListKey => "OwnerList";

        public static string GetKey(int OwnerId) => $"Owner-{OwnerId}";
    }
}
