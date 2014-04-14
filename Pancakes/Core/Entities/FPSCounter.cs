using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;

namespace WaffleCat.Core.Entities
{
    class FPSCounter : GUITextLabel
    {
        public FPSCounter(Vector2 pos)
            :base("0 FPS", pos)
        {
        }
    }
}
