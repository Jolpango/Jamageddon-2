using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Interfaces;
using MonoGame.Jolpango.Utilities;
using MonoGame.Jolpango.Graphics.Dispersion;

namespace MonoGame.Jolpango.Graphics.Effects
{
    public class FireEffect : IJDrawable, IJUpdatable
    {
        private float size;
        private ParticleEmitter redEmitter;
        private ParticleEmitter orangeEmitter;
        private ParticleEmitter smokeEmitter;
        private ParticleEmitter whiteCenterEmitter;

        public Vector2 Position { get; set; }

        public float Size
        {
            get { return size; }
            set { size = value; }
        }
        private float layerDepth;
        public float LayerDepth
        {
            get
            {
                return layerDepth;
            }
            set
            {
                smokeEmitter.LayerDepth = value + 0.01f;
                redEmitter.LayerDepth = value + 0.02f;
                orangeEmitter.LayerDepth = value + 0.03f;
                whiteCenterEmitter.LayerDepth = value + 0.05f;
                layerDepth = value;
            }
        }
        private Game game;
        public FireEffect(Game game, Texture2D fireTexture = null, Texture2D smokeTexture = null)
        {
            this.game = game;
            smokeEmitter = JJsonLoader.ReadParticleEmitterFromJson(EmitterSettings.SmokeJson, smokeTexture, game);
            whiteCenterEmitter = JJsonLoader.ReadParticleEmitterFromJson(EmitterSettings.FireCenterJson, fireTexture, game);
            redEmitter = JJsonLoader.ReadParticleEmitterFromJson(EmitterSettings.FireRedJson, fireTexture, game);
            orangeEmitter = JJsonLoader.ReadParticleEmitterFromJson(EmitterSettings.FireOrangeJson, fireTexture, game);
            size = 1;
            layerDepth = 0.5f;
        }
        public void Emit(Vector2 direction, int amount = 1)
        {
            setDirection(direction, (DispersionInverseCone)smokeEmitter.DispersionMethod);
            setDirection(direction, (DispersionInverseCone)redEmitter.DispersionMethod);
            setDirection(direction, (DispersionInverseCone)orangeEmitter.DispersionMethod);
            setDirection(direction, (DispersionInverseCone)whiteCenterEmitter.DispersionMethod);
            smokeEmitter.Emit(Position, amount);
            redEmitter.Emit(Position, amount);
            whiteCenterEmitter.Emit(Position, amount);
            orangeEmitter.Emit(Position, amount);
        }
        private void setDirection(Vector2 direction, DispersionInverseCone dispersion)
        {
            dispersion.Direction = direction;
        }
        public void Update(GameTime gameTime)
        {
            redEmitter.Update(gameTime);
            orangeEmitter.Update(gameTime);
            whiteCenterEmitter.Update(gameTime);
            smokeEmitter.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            smokeEmitter.Draw(spriteBatch);
            redEmitter.Draw(spriteBatch);
            orangeEmitter.Draw(spriteBatch);
            whiteCenterEmitter.Draw(spriteBatch);
        }
    }
}
