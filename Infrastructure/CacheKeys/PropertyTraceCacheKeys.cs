using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheKeys
{
    public static class PropertyTraceCacheKeys
    {
        public static string ListKey => "PropertyTraceList";

        public static string GetKey(int PropertyTraceId) => $"PropertyTrace-{PropertyTraceId}";
    }
}
