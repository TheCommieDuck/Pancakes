using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Systems
{
    public abstract class Subsystem
    {
        //when an entity has changed, then all subsystems should be updated to see if they want the given entity
        public Signature Signature { get; protected set; }

        protected List<Entity> entities;

        public MessageCollection Messages;

        public Subsystem(params Type[] signature)
        {
            RegisterInterestingComponents(signature);
            entities = new List<Entity>();
            Messages = new MessageCollection();
        }

        public void RegisterInterestingComponents(params Type[] signature)
        {
            Signature = new Signature(signature);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Entity e in entities)
                Process(e, gameTime);
        }

        public virtual void Process(Entity entity, GameTime gameTime) {}

        public virtual void InitializeEntity(Entity entity) {}

        public void AddEntity(Entity e)
        {
            Log.Write("Adding entity {0} to system {1}", e.ID, this.GetType());
            entities.Add(e);
            InitializeEntity(e);
        }

        protected void ClearMessages()
        {
            Messages.Clear();
        }
    }
}
