using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Components;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Systems
{
    public class FPSCameraSystem : Subsystem
    {
        public FPSCameraSystem()
            :base(typeof(CameraComponent), typeof(Transform3DComponent), typeof(FPSCameraComponent)) 
        {
            Messageboard.AddListener(this, typeof(RotationMessage));
        }

        public override void ProcessMessages(GameTime gameTime)
        {
            foreach (RotationMessage rotMsg in Messages.GetMessages<RotationMessage>())
                Rotate(rotMsg.Source.Get<Transform3DComponent>(), rotMsg.Rotation * (float)gameTime.ElapsedGameTime.TotalSeconds);

            Messages.Clear();
        }


        private void Rotate(Transform3DComponent trans, Vector3 rot)
        {
            trans.Rotation += rot;
            trans.Rotation = new Vector3(MathHelper.Clamp(trans.Rotation.X, -MathHelper.PiOver2 + 0.01f, MathHelper.PiOver2 - 0.01f), trans.Rotation.Y, trans.Rotation.Z);
            //trans.Rotation = new Vector3(MathHelper.WrapAngle(trans.Rotation.X + rot.X), MathHelper.WrapAngle(trans.Rotation.Y + rot.Y),
            //    MathHelper.WrapAngle(trans.Rotation.Z + rot.Z));
        }
    }
}
