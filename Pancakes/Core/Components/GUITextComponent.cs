using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Components;

namespace WaffleCat.Core.Components
{
    class GUITextComponent : Component
    {
        public SpriteFont Font { get; set; }

        public String Text { get; set; }

        public GUITextComponent()
        {
            Font = Blackboard.Get<ContentManager>("ContentManager").Load<SpriteFont>("MyFont");
            Text = "";
        }
    }
}
