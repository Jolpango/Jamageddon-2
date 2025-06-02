using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Utilities;
using Jamageddon2.GameScreens;
using MonoGame.Jolpango.Input;

namespace Jamageddon2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public JTimerHandler TimerHandler { get; set; }
        public MouseManager MouseManager { get; set; }
        public KeyboardManager KeyboardManager { get; set; }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();
            ScreenManager.Instance.Initialize(this);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TimerHandler = new JTimerHandler(this);
            MouseManager = new MouseManager();
            KeyboardManager = new KeyboardManager();
            ScreenManager.Instance.Initialize(this);
            ScreenManager.Instance.AddScreen(new DemoScreen1(this));
            Components.Add(TimerHandler);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
            MouseManager.Update();
            KeyboardManager.Update();
            ScreenManager.Instance.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));
            spriteBatch.Begin();
            ScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}