using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;

namespace WaffleCat
{
    class FloorComponent : Component
    {
        public static Color[] FloorColors = new Color[] { Color.White, Color.Red, Color.Yellow };

        public uint Width { get; set; }

        public uint Height { get; set; }
    }
}
