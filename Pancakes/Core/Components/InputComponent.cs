using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WaffleCat.Core.Input;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Components
{
    class InputComponent : Component
    {
        public delegate Message CreateMessage(Vector3? move = null);

        public Dictionary<InputEvent, CreateMessage> InputEvents { get; set; }

        public InputComponent()
        {
            InputEvents = new Dictionary<InputEvent, CreateMessage>();
        }
    }
}
