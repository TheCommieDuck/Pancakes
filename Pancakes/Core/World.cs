using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Graphics;
using WaffleCat.Core.Systems;

namespace WaffleCat.Core
{
    class World
    {
        public List<Subsystem> Systems { get; private set; }

        public List<IRenderer> RenderSystems { get; private set; }

        public World()
        {
            Systems = new List<Subsystem>();
            RenderSystems = new List<IRenderer>();
        }

        public virtual void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            foreach (Subsystem sys in Systems)
            {
                sys.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (IRenderer sys in RenderSystems)
                sys.Draw();
        }

        public void RegisterSystem(Subsystem sys)
        {
            Systems.Add(sys);
            if (sys is IRenderer)
                RenderSystems.Add((IRenderer)sys);
        }

        public void AddEntity(Entity e)
        {
            foreach (Subsystem s in Systems)
            {
                if (e.Signature.ComponentTypes.Intersect(s.Signature.ComponentTypes).Count() == s.Signature.ComponentTypes.Count())
                    s.AddEntity(e);
            }
        }
    }
}
