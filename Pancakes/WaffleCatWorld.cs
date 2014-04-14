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
            this.RegisterSystem(new MovementSystem());
            this.RegisterSystem(new FPSCounterSystem());
            this.RegisterSystem(new PositionalTrackingSystem());
            this.RegisterSystem(new CubeSystem());

            this.RegisterSystem(new TextRenderer());
            this.RegisterSystem(new VertexBufferRenderer());
            this.RegisterSystem(new ModelRenderer());
        }

        public override void Initialize()
        {

            this.RegisterSystems();

            FPSCamera camera = new FPSCamera(new Vector3(0.5f, 0.5f, 0.5f), Vector3.Zero, 
                Blackboard.Get<GraphicsDevice>("GraphicsDevice").Viewport.AspectRatio, 0.05f, 100f);

            Cube cube = new Cube();
            cube.Get<Transform3DComponent>().Position = new Vector3(5f, 1f, 5f);

            Entity fpscounter = new Entity();
            fpscounter.AddComponent(new Transform2DComponent() { Position = new Vector2(5, 150) });
            fpscounter.AddComponent(new FPSCounterComponent());
            fpscounter.AddComponent(new GUITextComponent());

            Entity pos = new Entity();
            pos.AddComponent(new PositionalTrackingComponent() { Target = camera.Get<Transform3DComponent>() });
            pos.AddComponent(new GUITextComponent());
            pos.AddComponent(new Transform2DComponent() { Position = new Vector2(5, 180) });

            Entity floor = new Entity();
            floor.AddComponent(new Transform3DComponent(new Vector3(0, 0, 0)));
            floor.AddComponent(new FloorComponent() { Width = 20, Height = 24 });
            floor.AddComponent(new VertexBufferComponent());

            Entity model = new Entity();
            model.AddComponent(new Transform3DComponent(new Vector3(10f, 0f, 5f)));
            model.AddComponent(new ModelComponent() { Model = Blackboard.Get<ContentManager>("ContentManager").Load<Model>("madokadddd") });

            this.AddEntity(model);
            this.AddEntity(pos);
            this.AddEntity(fpscounter);
            this.AddEntity(camera);
            this.AddEntity(floor);
            this.AddEntity(cube);
        }
    }
}
