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
    class CameraSystem : Subsystem
    {
        public CameraSystem()
            :base(typeof(CameraComponent), typeof(Transform3DComponent))
        {
            Messageboard.AddListener(this, typeof(HasMovedMessage));
        }

        //TODO: this is ugly
        public override void ProcessMessages(GameTime gameTime)
        {
            foreach (HasMovedMessage message in Messages.GetMessages<HasMovedMessage>())
            {
                foreach (Entity e in entities)
                {
                    if (e.Components.ContainsValue(message.Component))
                    {
                        e.Get<CameraComponent>().NeedsViewUpdate = true;
                        break;
                    }
                }
            }
            Messages.ClearMessages<HasMovedMessage>();
        }

        public override void Process(Entity entity, GameTime gameTime)
        {
            CameraComponent camera = entity.Get<CameraComponent>();
            //if we need to update either
            if (camera.NeedsFOVUpdate)
            {
                CalculateFOVMatrix(camera, entity.Get<Transform3DComponent>().Position);
                camera.NeedsViewUpdate = true;
            }

            if (camera.NeedsViewUpdate)
                CalculateViewMatrix(camera, entity.Get<Transform3DComponent>().Position, entity.Get<Transform3DComponent>().RotationMatrix);
        }

        private void CalculateFOVMatrix(CameraComponent camera, Vector3 position)
        {
            camera.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, camera.AspectRatio, camera.NearClip, camera.FarClip);
            camera.NeedsFOVUpdate = false;
        }

        private void CalculateViewMatrix(CameraComponent camera, Vector3 position, Matrix rotationMatrix)
        {
            Vector3 camUp = Vector3.Transform(CameraComponent.BaseReference, rotationMatrix);
            Vector3 lookAt =
            camera.LookAt = position + Vector3.Transform(CameraComponent.BaseReference, rotationMatrix);
            camera.ViewMatrix = Matrix.CreateLookAt(position, camera.LookAt, Vector3.Up);
            camera.NeedsViewUpdate = false;
        }

        public override void InitializeEntity(Entity entity)
        {
            Process(entity, null);
            Blackboard.Set("Camera", entity);
        }
    }
}
