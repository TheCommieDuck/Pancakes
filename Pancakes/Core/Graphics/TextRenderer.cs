using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Systems;

namespace WaffleCat.Core.Graphics
{
    class TextRenderer : Subsystem, IRenderer
    {
        public SpriteBatch SpriteBatch { get; private set; }

        public TextRenderer()
            : base(typeof(Transform2DComponent), typeof(GUITextComponent))
        {
            SpriteBatch = new SpriteBatch(Blackboard.Get<GraphicsDevice>("GraphicsDevice"));
        }

        public void Draw()
        {
            SpriteBatch.Begin();
            foreach (Entity e in entities)
            {
                SpriteBatch.DrawString(e.Get<GUITextComponent>().Font, e.Get<GUITextComponent>().Text, e.Get<Transform2DComponent>().Position, 
                    Color.White);
            }
            SpriteBatch.End();
        }
    }
}
