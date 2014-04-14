using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Components
{
    class CameraComponent : Component
    {
        public static Vector3 BaseReference = new Vector3(0, 0, 1);

        public float AspectRatio
        {
            get { return aspectRatio; }
            set
            {
                aspectRatio = value;
                NeedsFOVUpdate = true;
            }
        }

        public float NearClip
        {
            get { return nearClip; }
            set
            {
                nearClip = value;
                NeedsFOVUpdate = true;
            }
        }

        public float FarClip
        {
            get { return farClip; }
            set
            {
                farClip = value;
                NeedsFOVUpdate = true;
            }
        }

        public Matrix Projection { get; set; }

        public Vector3 LookAt
        {
            get { return lookAt; }
            set
            {
                lookAt = value;
                NeedsViewUpdate = true;
            }
        }

        public bool NeedsViewUpdate { get; set; }

        public bool NeedsFOVUpdate { get; set; }

        public Matrix ViewMatrix { get; set; }

        private float aspectRatio;

        private float nearClip;

        private float farClip;

        private Vector3 lookAt;

        public CameraComponent(float aspect, float near, float far)
        {
            AspectRatio = aspect;
            NearClip = near;
            FarClip = far;
        }
    }
}
