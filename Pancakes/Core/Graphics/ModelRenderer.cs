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
    class ModelRenderer : Subsystem, IRenderer
    {
        public GraphicsDevice GraphicsDevice { get; private set; }

        public ModelRenderer()
            :base(typeof(Transform3DComponent), typeof(ModelComponent))
        {
            this.GraphicsDevice = Blackboard.Get<GraphicsDevice>("GraphicsDevice");
        }

        public override void InitializeEntity(Entity entity)
        {
            ModelComponent model = entity.Get<ModelComponent>();
            model.ModelTransforms = new Matrix[model.Model.Bones.Count];
            model.Model.CopyAbsoluteBoneTransformsTo(model.ModelTransforms);

            foreach (ModelMesh mesh in model.Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    if (part.Effect is BasicEffect)
                    {
                        BasicEffect eff = (BasicEffect)part.Effect;
                        part.Tag = new MeshTag(eff.DiffuseColor, eff.Texture, eff.SpecularPower){ CachedEffect = eff};
                    }
                }
            }
        }

        public void Draw()
        {
            FPSCamera camera = Blackboard.Get("Camera") as FPSCamera;

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.BlendState = BlendState.Opaque;

            Matrix view = camera.Get<CameraComponent>().ViewMatrix;
            Matrix proj = camera.Get<CameraComponent>().Projection;
            foreach (Entity e in this.entities)
            {
                Matrix entityWorld = Matrix.CreateScale(0.5f) * e.Get<Transform3DComponent>().TransformMatrix;
                foreach (ModelMesh mesh in e.Get<ModelComponent>().Model.Meshes)
                {
                    Matrix world = e.Get<ModelComponent>().ModelTransforms[mesh.ParentBone.Index] * entityWorld;
                    foreach(ModelMeshPart meshPart in mesh.MeshParts)
                    {
                        Effect effect = meshPart.Effect;
                        if (meshPart.Effect is BasicEffect)
                        {
                            BasicEffect basicEffect = (BasicEffect)effect;
                            basicEffect.View = view;
                            basicEffect.Projection = proj;
                            basicEffect.World = world;
                            basicEffect.EnableDefaultLighting();
                        }
                        else
                        {
                            effect.Parameters["World"].SetValue(world);
                            effect.Parameters["Projection"].SetValue(proj);
                            effect.Parameters["View"].SetValue(view);
                            effect.Parameters["DiffuseColor"].SetValue(new Vector3(0f, 1f, 1f));
                        }
                    }
                    mesh.Draw();
                }
            }
        }
    }
}
