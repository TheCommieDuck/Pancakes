using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Systems;

namespace WaffleCat.Core.Graphics
{
    class VertexBufferRenderer : Subsystem, IRenderer
    {
        public GraphicsDevice GraphicsDevice { get; private set; }

        public VertexBufferRenderer()
            :base(typeof(Transform3DComponent), typeof(VertexBufferComponent))
        {
            this.GraphicsDevice = Blackboard.Get<GraphicsDevice>("GraphicsDevice");
        }

        public void Draw()
        {
            Draw(Blackboard.Get<BasicEffect>("BasicEffect"));
        }

        public void Draw(BasicEffect effect)
        {
            FPSCamera camera = Blackboard.Get("Camera") as FPSCamera;

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.BlendState = BlendState.Opaque;

            effect.VertexColorEnabled = true;
            effect.View = camera.Get<CameraComponent>().ViewMatrix;
            effect.Projection = camera.Get<CameraComponent>().Projection;
            foreach (Entity e in this.entities)
            {
                effect.World = e.Get<Transform3DComponent>().TransformMatrix;
                VertexBuffer buffer = e.Get<VertexBufferComponent>().Buffer;
                GraphicsDevice.SetVertexBuffer(buffer);
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, buffer.VertexCount / 3);
                }
            }
        }
    }
}
