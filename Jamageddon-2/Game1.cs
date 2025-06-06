using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Content;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.UI.Elements;
using MonoGame.Jolpango.UI.Elements.Containers;
using MonoGame.Jolpango.Utilities;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Jamageddon2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private JGameScene scene;
        public JTimerHandler TimerHandler { get; set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TimerHandler = new JTimerHandler(this);
            Components.Add(TimerHandler);
            JTextureCache.Initialize(GraphicsDevice);

            scene = new JGameScene(this);
            scene.SetPhysicsSystem(new JTopDownPhysicsSystem());


            scene.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            scene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));
            scene.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}