#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using WaffleCat.Core;
using WaffleCat.Core.Graphics;
#endregion

namespace WaffleCat
{
    
    public class WaffleCatGame : Game
    {
        //updating
        private double t = 0.0d;
        private double dt = 0.01d;
        private double accumulator = 0.0d;
        private double alpha = 0.0d;

        //fps
        private int[] frames;
        private int currIndex = 0;
        private int frameCount;
        private TimeSpan elapsed = TimeSpan.Zero;
        private GraphicsDeviceManager graphics;

        private WaffleCatEngine engine;

        public WaffleCatGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 960;
            this.IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;

            graphics.ApplyChanges();
            engine = new WaffleCatEngine();
        }

        protected override void Initialize()
        {
            frames = new int[(int)(1 / dt)];
            engine.World = new WaffleCatWorld();
            engine.Initialize(graphics);
            Content = engine.ContentManager;
            //lolhacks
            Window.SetPosition(new Point(-6, 0));
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
            Log.Close();
            Exit();
        }

        protected override void Update(GameTime gameTime)
        {
            //TODO: Find the proper way to work framerate
            //Console.WriteLine(1 / gameTime.ElapsedGameTime.TotalSeconds);
            accumulator += gameTime.ElapsedGameTime.TotalSeconds;
            elapsed += gameTime.ElapsedGameTime;
            if (elapsed > TimeSpan.FromSeconds(dt))
            {
                elapsed -= TimeSpan.FromSeconds(dt);
                //so we update this slice
                frames[currIndex] = frameCount;
                currIndex++;
                currIndex %= (int)(1 / dt);
                int fps = 0;
                foreach (int i in frames)
                    fps += i;
                Blackboard.Set("FPS", fps);
                frameCount = 0;
            }

            while (accumulator >= dt)
            {
                GameTime time = new GameTime(TimeSpan.FromSeconds(t), TimeSpan.FromSeconds(dt));
                engine.Update(time);
                base.Update(time);
                t += dt;
                accumulator -= dt;
            }
            alpha = accumulator / dt;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            frameCount++;
            base.Draw(gameTime);
            engine.Draw(gameTime);
        }
    }
}
