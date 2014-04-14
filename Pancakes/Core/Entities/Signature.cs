using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaffleCat.Core.Entities
{
    class Signature
    {
        public List<Type> ComponentTypes;

        public Signature(params Type[] types)
        {
            ComponentTypes = new List<Type>();
            ComponentTypes.AddRange(types);
        }

        public bool Matches(Signature sig)
        {
            if (ComponentTypes.Intersect(sig.ComponentTypes).Count() == sig.ComponentTypes.Count())
                return true;
            else
                return false;
        }
    }
}