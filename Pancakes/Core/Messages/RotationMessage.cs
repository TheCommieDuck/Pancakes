using Microsoft.Xna.Framework;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Messages
{
    class RotationMessage : Message
    {
        public Vector3 Rotation;
        public RotationMessage(Entity source)
        {
            Source = source;
        }
    }
}
