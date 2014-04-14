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
    class MovementSystem : Subsystem
    {

        public MovementSystem()
            : base(typeof(Transform3DComponent))
        {
            Messageboard.AddListener(this, typeof(MovementMessage));
            Messageboard.AddListener(this, typeof(RotationMessage));
            Messageboard.AddListener(this, typeof(HasMovedMessage));
        }

        public override void InitializeEntity(Entity entity)
        {
            UpdateTransformMatrix(entity.Get<Transform3DComponent>());
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MovementMessage moveMsg in Messages.GetMessages<MovementMessage>())
                Move(moveMsg.Source.Get<Transform3DComponent>(), Vector3.Multiply(moveMsg.MovementVector, 
                    (float)gameTime.ElapsedGameTime.TotalSeconds));
            
            foreach (RotationMessage rotMsg in Messages.GetMessages<RotationMessage>())
                Rotate(rotMsg.Source.Get<Transform3DComponent>(), rotMsg.Rotation * (float)gameTime.ElapsedGameTime.TotalSeconds);

            foreach (HasMovedMessage moveMsg in Messages.GetMessages<HasMovedMessage>())
                UpdateTransformMatrix(((HasMovedMessage)moveMsg).Component);
            Messages.Clear();

            base.Update(gameTime);
        }

        public void Move(Transform3DComponent pos, Vector3 move)
        {
            pos.Position += Vector3.Transform(move, Matrix.CreateFromYawPitchRoll(pos.Rotation.Y, pos.Rotation.X, pos.Rotation.Z));
        }

        private void Rotate(Transform3DComponent trans, Vector3 rot)
        {
            trans.Rotation += rot;
            /*trans.Rotation = new Vector3(MathHelper.WrapAngle(trans.Rotation.X - rot.X), MathHelper.WrapAngle(trans.Rotation.Y - rot.Y),
                MathHelper.WrapAngle(trans.Rotation.Z - rot.Z));*/
        }

        private void UpdateTransformMatrix(Transform3DComponent trans)
        {
            trans.TransformMatrix = Matrix.CreateFromYawPitchRoll(0, 0, 0) * Matrix.CreateTranslation(trans.Position);
        }
    }
}
