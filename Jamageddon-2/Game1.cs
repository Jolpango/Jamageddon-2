using Jamageddon2.Scenes;
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
using System.Collections.Generic;
using Jamageddon2.Entities.Enemies;
using Jamageddon2.Entities.Path;

namespace Jamageddon2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private JSceneManager sceneManager = new JSceneManager();
        public JTimerHandler TimerHandler { get; set; }

        private List<Vector2> pathWaypoints;
        private JTomatoEnemy tomatoEnemy;
        private UIButton startButton;
        private UIButton stopButton;

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
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TimerHandler = new JTimerHandler(this);
            Components.Add(TimerHandler);
            JTextureCache.Initialize(GraphicsDevice);
            sceneManager.AddScene(new MenuScene(this) { Parent = sceneManager });

            scene = new JGameScene(this);
            scene.SetPhysicsSystem(new JTopDownPhysicsSystem());
            Console.WriteLine("Scene created - console");

            // Create path
            var path = new JPathComponent();
            path.SetSpawnPoint(new Vector2(0, 200));
            path.AddWaypoint(new Vector2(400, 200));
            path.AddWaypoint(new Vector2(400, 400));

            // Create tomato enemy
            tomatoEnemy = new JTomatoEnemy();
            tomatoEnemy.SetPath(path);
            scene.AddEntity(tomatoEnemy);

            // Create UI container for buttons
            var buttonContainer = new UIContainer
            {
                Position = new Vector2(10, 10),
                Size = new Vector2(320, 50)
            };

            // Create start button
            startButton = new UIButton();
            startButton.Position = new Vector2(0, 0);
            startButton.Size = new Vector2(150, 40);
            startButton.OnClick += (button) => tomatoEnemy.StartMovement();
            buttonContainer.AddChild(startButton);

            // Create stop button
            stopButton = new UIButton();
            stopButton.Position = new Vector2(160, 0);
            stopButton.Size = new Vector2(150, 40);
            stopButton.OnClick += (button) => tomatoEnemy.StopMovement();
            buttonContainer.AddChild(stopButton);

            scene.AddUIElement(buttonContainer);

            scene.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));
            sceneManager.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
