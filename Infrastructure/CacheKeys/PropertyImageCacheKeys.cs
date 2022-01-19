using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CacheKeys
{
    public static class PropertyImageCacheKeys
    {
        public static string ListKey => "PropertyImageList";

        public static string GetKey(int PropertyImageId) => $"PropertyImage-{PropertyImageId}";
    }
}
