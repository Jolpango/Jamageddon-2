using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JParticleEffectComponent : JComponent, IJInjectable<Game>
    {
        private ParticleEmitter emitter;
        private Game game;
        private string emitterPath;

        public void Inject(Game service)
        {
            game = service;
        }
        public JParticleEffectComponent(string emitterPath)
        {
            this.emitterPath = emitterPath;
        }

        public override void LoadContent()
        {
            emitter = JJsonLoader.ReadParticleEmitterFromFile(emitterPath, game);
            base.LoadContent();
        }

        public void Emit(Vector2 position, int amount)
        {
            emitter.Emit(position, amount);
        }
        public override void Update(GameTime gameTime)
        {
            emitter.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            emitter.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
