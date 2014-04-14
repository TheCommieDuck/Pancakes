using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaffleCat.Core.Components
{
    class ComponentCollection : Dictionary<Type, Component>
    {
        public Component Get<T>()
        {
            return this[typeof(T)];
        }

        public void Add(Component c)
        {
            this.Add(c.GetType(), c);
        }
    }
}
