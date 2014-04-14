using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Graphics;
using WaffleCat.Core.Messages;

namespace WaffleCat.Core.Systems
{
    class GraphicsSystem : Subsystem
    {
        public static Dictionary<string, Vector2> Resolutions = new Dictionary<string, Vector2>()
        {
            { "800x600", new Vector2(800,600)},
            { "1024x768", new Vector2(1024, 768)},
            {"1280x960", new Vector2(1280, 960)},
            {"1280x1024", new Vector2(1280, 1024)}
        };

        public Color ClearColor { get; set; }

        public Vector2 CurrentResolution
        {
            get
            {
                return this.GraphicsDeviceManager.IsFullScreen ? res : windowedRes;
            }

            set
            {
                res = value;
                this.GraphicsDeviceManager.PreferredBackBufferWidth = (int)value.X;
                this.GraphicsDeviceManager.PreferredBackBufferHeight = (int)value.Y;
                GraphicsDeviceManager.ApplyChanges();
            }
        }

        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        private Vector2 windowedRes;
        private Vector2 res;

        public GraphicsSystem(GraphicsDeviceManager graphics)
        {
            this.GraphicsDeviceManager = graphics;
            this.CurrentResolution = new Vector2(1920, 1080);
            this.windowedRes = new Vector2(1024, 768);
        }

        public override void Update(GameTime gameTime)
        {
            if (Messages.HasMessages)
            {
                if (Messages.GetMessages<ToggleFullscreenMessage>().Count() > 0)
                    ToggleFullscreen();
                Messages.Clear();
            }
            base.Update(gameTime);
        }

        public void ToggleFullscreen()
        {
            SetFullscreen(!GraphicsDeviceManager.IsFullScreen);
        }

        public void SetFullscreen(bool lolcatsEverywhere)
        {
            GraphicsDeviceManager.IsFullScreen = lolcatsEverywhere;
            if (lolcatsEverywhere)
                this.CurrentResolution = new Vector2(1920, 1080);
            else
                this.CurrentResolution = new Vector2(1024, 768);
            GraphicsDeviceManager.ApplyChanges();
        }
    }
}
