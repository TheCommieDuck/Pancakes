using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaffleCat.Core.Components;

namespace WaffleCat.Core.Messages
{
    class HasMovedMessage : Message
    {
        public Transform3DComponent Component;

        public HasMovedMessage(Transform3DComponent comp)
        {
            Component = comp;
        }
    }
}
