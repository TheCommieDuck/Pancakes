using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Systems
{
    class FPSCounterSystem : Subsystem
    {
        private int count = 5;
        private int curr = 0;
        public FPSCounterSystem()
            :base(typeof(FPSCounterComponent), typeof(GUITextComponent))
        {
        }

        public override void Process(Entity entity, GameTime gameTime)
        {
            if (curr == count)
            {
                entity.Get<GUITextComponent>().Text = Blackboard.Get<int>("FPS").ToString();
                curr = 0;
            }
            curr++;
        }
    }
}
