using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaffleCat.Core.Components;

namespace WaffleCat.Core.Entities
{
    class Entity
    {
        private static uint CurrentID = 0;

        public static uint GetNextID()
        {
            return CurrentID++;
        }

        public uint ID { get; private set; }

        public Signature Signature { get; private set; }

        public ComponentCollection Components { get; private set; }

        public T Get<T>() where T : Component
        {
            return (T)Components[typeof(T)];
        }

        public Entity()
        {
            ID = Entity.GetNextID();
            Signature = new Signature();
            Components = new ComponentCollection();
            Log.Write("Creating new entity with ID {0}", this.ID);
        }

        public void AddComponent(Component component)
        {
            Log.Write("Adding Component {0} to entity {1}", component.GetType(), ID);
            Signature.ComponentTypes.Add(component.GetType());
            Components.Add(component);
        }
    }
}
