using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheKeys
{
    public static class PropertyCacheKeys
    {
        public static string ListKey => "PropertyList";

        public static string GetKey(int propertyId) => $"Property-{propertyId}";
    }
}
