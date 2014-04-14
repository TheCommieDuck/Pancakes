using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Components
{
    class VertexBufferComponent : Component
    {
        public VertexBuffer Buffer { get; set; }
    }
}
