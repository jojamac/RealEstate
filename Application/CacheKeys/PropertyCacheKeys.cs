using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CacheKeys
{
   public  class PropertyCacheKeys
    {
        public static string ListKey => "PropertyList";

        public static string GetKey(int propertyId) => $"Property-{propertyId}";
    }
}
