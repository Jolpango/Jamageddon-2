using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Graphics.Effects;

namespace Jamageddon2.GameScreens
{
    public class DemoScreen2 : GameScreen
    {
        private FireEffect fireEffect;
        public DemoScreen2(Game1 game) : base(game)
        {
        }

        public override void LoadContent()
        {
            fireEffect = new FireEffect(game);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.KeyboardManager.IsKeyClicked(Keys.Back))
            {
                ScreenManager.Instance.PopScreen();
            }
            if(game.MouseManager.IsLeftButtonClicked())
            {
                fireEffect.Position = game.MouseManager.Position;
                fireEffect.Emit(-Vector2.UnitY, 10);
            }
            fireEffect.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fireEffect.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
