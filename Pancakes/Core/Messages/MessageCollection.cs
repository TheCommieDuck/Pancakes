using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaffleCat.Core.Messages
{
    public class MessageCollection
    {
        private Dictionary<Type, List<Message>> messages;

        public bool HasMessages
        {
            get
            {
                foreach (List<Message> msg in messages.Values)
                {
                    if (msg.Count() > 0)
                        return true;
                }
                return false;
            }
        }

        public MessageCollection()
        {
            messages = new Dictionary<Type, List<Message>>();
        }

        public void Clear()
        {
            foreach (List<Message> m in messages.Values)
                m.RemoveAll(x => true);
        }

        public void ClearMessages<T>()
        {
            messages[typeof(T)] = new List<Message>();
        }

        public void AddMessage(Message msg)
        {
            Type type = msg.GetType();
            List<Message> currMsgs;
            messages.TryGetValue(type, out currMsgs);
            if (currMsgs == null)
                messages[type] = new List<Message>() { msg };
            else
                currMsgs.Add(msg);
        }

        public IEnumerable<T> GetMessages<T>() where T : Message
        {
            List<Message> list;
            messages.TryGetValue(typeof(T), out list);
            if (list == null)
                return new List<T>();
            else
            {
                return list.Select(x => (T)x);
            }
        }
    }
}
