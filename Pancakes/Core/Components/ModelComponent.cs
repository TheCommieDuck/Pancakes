using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaffleCat.Core.Components
{
    class ModelComponent : Component
    {
        public Model Model { get; set; }

        public Matrix[] ModelTransforms { get; set; }


    }
}
