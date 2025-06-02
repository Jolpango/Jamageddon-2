using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Graphics.Sprites;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2.GameScreens
{
    public class DemoScreen1 : GameScreen
    {
        private ParticleEmitter sparkleEmitter;
        private JTimer sparkleTimer;
        private JSprite sprite;
        public DemoScreen1(Game1 game) : base(game)
        {
            sprite = new JSprite();
        }

        public override void LoadContent()
        {
            sprite.LoadContent(game.Content, "Content/Animation/axe.json");
            sparkleEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/random.json", game);
            sparkleTimer = new JTimer(0.05f, Sparkle, isRepeating: true);
            base.LoadContent();
        }

        private void Sparkle()
        {
            var mouseState = Mouse.GetState();
            sparkleEmitter.Emit(mouseState.Position.ToVector2(), 1);
        }

        public override void Update(GameTime gameTime)
        {
            if (game.MouseManager.IsLeftButtonClicked())
            {
                sprite.StartAnimation("Default");
            }
            if (game.KeyboardManager.IsKeyClicked(Keys.Enter))
            {
                ScreenManager.Instance.AddScreen(new DemoScreen2(game), pop: false);
            }
            sprite.Position = game.MouseManager.Position;
            sprite.Update(gameTime);
            sparkleEmitter.Update(gameTime);
            sparkleTimer.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            sparkleEmitter.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
