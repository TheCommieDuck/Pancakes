using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;

namespace WaffleCat.Core.Entities
{
    class Cube : Entity
    {
        public Cube()
        {
            this.AddComponent(new Transform3DComponent() { Position = new Vector3(5f, 0.5f, 15f) });
            this.AddComponent(new VertexBufferComponent());
            this.AddComponent(new CubeComponent() { Size = 0.5f });
        }
    }
}
