using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Components
{
    class Transform3DComponent : Component
    {
        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                Messageboard.PostMessage(new HasMovedMessage(this));
            }
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                Messageboard.PostMessage(new HasMovedMessage(this));
            }
        }

        public Matrix RotationMatrix
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);
            }
        }

        public Matrix TransformMatrix { get; set; }

        private Vector3 position;

        private Vector3 rotation;

        public Transform3DComponent(Vector3 pos)
            : this(pos, Vector3.Zero) { }

        public Transform3DComponent()
            :this(Vector3.Zero, Vector3.Zero)
        {
        }

        public Transform3DComponent(Vector3 pos, Vector3 rot)
        {
            this.Position = pos;
            this.Rotation = rot;
            this.TransformMatrix = Matrix.Identity;
        }
    }
}
