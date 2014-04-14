using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaffleCat.Core
{
    public static class Blackboard
    {
        private static Dictionary<String, object> items = new Dictionary<string,object>();

        public static T Get<T>(String item)
        {
            object ret;
            items.TryGetValue(item, out ret);
            return ret == null ? default(T) : (T)ret;
        }

        public static object Get(String item)
        {
            return items[item];
        }

        public static void Set(String name, object item)
        {
            items[name] = item;
        }
    }
}
