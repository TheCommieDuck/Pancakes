using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Graphics;
using WaffleCat.Core.Systems;

namespace WaffleCat
{
    class WaffleCatWorld : World
    {
        public void RegisterSystems()
        {
            this.RegisterSystem(new CameraSystem());
            this.RegisterSystem(new FloorSystem());
            this.RegisterSystem(new InputSystem());
            this.RegisterSystem(new TransformSystem());
            this.RegisterSystem(new FPSCounterSystem());
            this.RegisterSystem(new PositionalTrackingSystem());
            this.RegisterSystem(new CubeSystem());
            this.RegisterSystem(new FPSCameraSystem());
            this.RegisterSystem(new TextRenderer());
            this.RegisterSystem(new VertexBufferRenderer());
            this.RegisterSystem(new ModelRenderer());
        }

        public override void Initialize()
        {
            this.RegisterSystems();

            FPSCamera camera = new FPSCamera(new Vector3(0f, 0f, 0f), Vector3.Zero, 
                Blackboard.Get<GraphicsDevice>("GraphicsDevice").Viewport.AspectRatio, 0.05f, 100f);

            Entity fpscounter = new Entity();
            fpscounter.AddComponent(new Transform2DComponent() { Position = new Vector2(5, 150) });
            fpscounter.AddComponent(new FPSCounterComponent());
            fpscounter.AddComponent(new GUITextComponent());

            Entity pos = new Entity();
            pos.AddComponent(new PositionalTrackingComponent() { Target = camera.Get<Transform3DComponent>() });
            pos.AddComponent(new GUITextComponent());
            pos.AddComponent(new Transform2DComponent() { Position = new Vector2(5, 180) });

            Entity model = new Entity();
            model.AddComponent(new Transform3DComponent(new Vector3(0f, -1f, 2f)));
            model.Get<Transform3DComponent>().Rotation = new Vector3(0f, MathHelper.PiOver2, 3f*MathHelper.PiOver2);
            Model m = Blackboard.Get<ContentManager>("ContentManager").Load<Model>("dragon");
            model.AddComponent(new ModelComponent() { Model = m });

            this.AddEntity(model);
            this.AddEntity(pos);
            this.AddEntity(fpscounter);
            this.AddEntity(camera);
        }
    }
}
