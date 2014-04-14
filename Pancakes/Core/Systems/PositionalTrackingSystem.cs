using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Systems
{
    class PositionalTrackingSystem : Subsystem
    {
        public PositionalTrackingSystem()
            : base(typeof(PositionalTrackingComponent), typeof(GUITextComponent))
        {
        }

        public override void Process(Entity entity, GameTime gameTime)
        {
            entity.Get<GUITextComponent>().Text = entity.Get<PositionalTrackingComponent>().Target.Position.ToString();
        }
    }
}
