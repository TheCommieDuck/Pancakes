using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Systems
{
    class TransformSystem : Subsystem
    {

        public TransformSystem()
            : base(typeof(Transform3DComponent))
        {
            Messageboard.AddListener(this, typeof(HasMovedMessage));
            Messageboard.AddListener(this, typeof(MovementMessage));
        }

        public override void InitializeEntity(Entity entity)
        {
            UpdateTransformMatrix(entity.Get<Transform3DComponent>());
        }

        public override void ProcessMessages(GameTime gameTime)
        {
            foreach (MovementMessage moveMsg in Messages.GetMessages<MovementMessage>())
                Move(moveMsg.Source.Get<Transform3DComponent>(), Vector3.Multiply(moveMsg.MovementVector,
                    (float)gameTime.ElapsedGameTime.TotalSeconds));

            foreach (HasMovedMessage moveMsg in Messages.GetMessages<HasMovedMessage>())
                UpdateTransformMatrix(((HasMovedMessage)moveMsg).Component);
            Messages.Clear();
        }

        public void Move(Transform3DComponent pos, Vector3 move)
        {
            pos.Position += Vector3.Transform(move, pos.RotationMatrix);
        }

        private void UpdateTransformMatrix(Transform3DComponent trans)
        {
            trans.TransformMatrix = trans.RotationMatrix * Matrix.CreateTranslation(trans.Position);
        }
    }
}
