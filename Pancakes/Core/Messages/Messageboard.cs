using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaffleCat.Core.Systems;

namespace WaffleCat.Core.Messages
{
    static class Messageboard
    {
        private static Dictionary<Type, List<Subsystem>> listeners = new Dictionary<Type,List<Subsystem>>();

        public static void PostMessage(Message msg)
        {
            List<Subsystem> interested;
            listeners.TryGetValue(msg.GetType(), out interested);
            if (interested != null)
            {
                foreach (Subsystem sys in interested)
                {
                    SendMessage(msg, sys);
                }
            }
        }

        public static void AddListener(Subsystem sys, params Type[] types)
        {
            foreach (Type t in types)
            {
                List<Subsystem> systems;
                listeners.TryGetValue(t, out systems);
                if (systems == null)
                    listeners[t] = new List<Subsystem>() { sys };
                else
                    listeners[t].Add(sys);
            }
        }

        private static void SendMessage(Message msg, Subsystem sys)
        {
            sys.Messages.AddMessage(msg);
        }
    }
}
