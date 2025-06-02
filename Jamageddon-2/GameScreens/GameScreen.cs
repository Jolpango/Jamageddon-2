using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jamageddon2.GameScreens
{
    public class GameScreen
    {
        protected Game1 game;
        public GameScreen(Game1 game)
        {
            this.game = game;
        }
        public virtual void LoadContent()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
