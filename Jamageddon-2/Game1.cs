using Jamageddon2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Utilities;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private JSceneManager sceneManager = new JSceneManager();
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
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TimerHandler = new JTimerHandler(this);
            Components.Add(TimerHandler);
            JTextureCache.Initialize(GraphicsDevice);
            JFontCache.Initialize(Content.Load<SpriteFont>("Fonts/default"));
            sceneManager.AddScene(new MenuScene(this) { Parent = sceneManager });

        }

        protected override void Update(GameTime gameTime)
        {
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
 