using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Entities
{
    class GUITextLabel : Entity
    {
        public GUITextLabel(String text, Vector2 position)
        {
            this.AddComponent(new GUITextComponent()
            { Font = Blackboard.Get<ContentManager>("ContentManager").Load<SpriteFont>("MyFont"), Text = text });
            this.AddComponent(new Transform2DComponent() { Position = position });
        }
    }
}
