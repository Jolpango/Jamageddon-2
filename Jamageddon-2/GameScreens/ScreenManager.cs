using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2.GameScreens
{
    public class ScreenManager
    {
        public static ScreenManager Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }
        private static ScreenManager instance;
        private Game1 game;
        private Stack<GameScreen> screens = new Stack<GameScreen>();
        public GameScreen CurrentScreen
        {
            get
            {
                return screens.Peek();
            }
        }
        private ScreenManager()
        {
            screens = new Stack<GameScreen>();
        }

        public GameScreen AddScreen(GameScreen screen, bool pop = false)
        {
            screen.LoadContent();
            GameScreen oldScreen = null;
            if (pop)
            {
                oldScreen = screens.Pop();
            }
            screens.Push(screen);
            return oldScreen;
        }

        public GameScreen PopScreen()
        {
            return screens.Pop();
        }

        public void Initialize(Game1 game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }
    }
}
