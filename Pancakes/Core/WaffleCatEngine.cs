using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Graphics;
using WaffleCat.Core.Systems;

namespace WaffleCat.Core
{
    class WaffleCatEngine
    {
        public GraphicsDevice GraphicsDevice { get; private set; }

        public WaffleCatContentManager ContentManager { get; private set; }

        public GameServiceContainer Services { get; private set; }

        public World World { get; set; }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            //Start the log
            Log.Initialize();
            Log.Write("Initializing the engine");
            GraphicsDevice = graphics.GraphicsDevice;
            Blackboard.Set("GraphicsDevice", GraphicsDevice);
            Blackboard.Set("BasicEffect", new BasicEffect(GraphicsDevice));

            Services = new GameServiceContainer();
            Services.AddService(typeof(IGraphicsDeviceManager), graphics);
            Services.AddService(typeof(IGraphicsDeviceService), graphics);

            //Add content manager
            ContentManager = new WaffleCatContentManager(Services);
            ContentManager.RootDirectory = "Content";
            Blackboard.Set("ContentManager", ContentManager);

            if (World == null)
                World = new World();
            World.Initialize();

        }

        public void Update(GameTime gameTime)
        {
            World.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            World.Draw();
        }
    }
}
