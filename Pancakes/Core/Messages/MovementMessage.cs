using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Messages
{
    class MovementMessage : Message
    {
        public Vector3 MovementVector { get; set; }

        public MovementMessage(Entity source)
        {
            Source = source;
        }
    }
}
