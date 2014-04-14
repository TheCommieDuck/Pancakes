using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Components
{
    class PositionalTrackingComponent : Component
    {
        public Transform3DComponent Target { get; set; }
    }
}
