using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Graphics;
using MonoGame.Jolpango.Utilities;
using MonoGame.Jolpango.UI;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Graphics.Transitions;
using MonoGame.Jolpango.Graphics.Dispersion;
using MonoGame.Jolpango.Graphics.Effects;
using MonoGame.Jolpango.Tiled;

namespace JolpangoSample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ParticleEmitter sparkleEmitter;
        private ParticleEmitter fireEmitter;
        private ParticleEmitter fireRedEmitter;
        private ParticleEmitter fireCenterEmitter;
        private ParticleEmitter smokeEmitter;
        private ParticleEmitter fireSparkEmitter;
        private FireEffect fireEffect;
        private Texture2D stick;

        public JTimerHandler TimerHandler { get; set; }
        private MonoGame.Jolpango.Graphics.Sprites.JSprite sprite;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            sprite = new MonoGame.Jolpango.Graphics.Sprites.JSprite();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            _graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TimerHandler = new JTimerHandler(this);
            Components.Add(TimerHandler);

            Texture2D tempTexture = new Texture2D(GraphicsDevice, 2, 2);
            Color[] data = new Color[2 * 2];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            tempTexture.SetData(data);
            fireEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/fire.json", this);
            fireRedEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/firered.json", this);
            fireSparkEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/firespark.json", this);
            smokeEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/smoke.json", this);
            sparkleEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/random.json", this);
            fireCenterEmitter = JJsonLoader.ReadParticleEmitterFromFile("Content/Emitters/firecenter.json", this);
            fire();
            smoke();
            sprite.LoadContent(Content, "Content/Animation/axe.json");
            sprite.StartAnimation("Normal", loop: true, onComplete: () => sparkle());
            stick = Content.Load<Texture2D>("stick");
            fireEffect = new FireEffect(this, Content.Load<Texture2D>("particle"), Content.Load<Texture2D>("smokeparticle"));
        }
        private void sparkle()
        {
            MouseState state = Mouse.GetState();
            Vector2 pos = new(state.X, state.Y);
            fireEffect.Position = pos;
            fireEffect.Emit(-Vector2.UnitY);
        }

        private void fire()
        {
            TimerHandler.AddTimer(.05f, () =>
            {
                Vector2 pos = new(375, 225);
                fireEffect.Position = pos;
                fireEffect.Emit(-Vector2.UnitY);
                fire();
            });
        }
        private void smoke()
        {
            TimerHandler.AddTimer(.2f, () =>
            {

                Vector2 pos = new(375, 225);
                smokeEmitter.Emit(pos, 1);
                smoke();
            });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
            sparkleEmitter.Update(gameTime);
            fireEmitter.Update(gameTime);
            smokeEmitter.Update(gameTime);
            fireRedEmitter.Update(gameTime);
            fireSparkEmitter.Update(gameTime);
            fireCenterEmitter.Update(gameTime);
            fireEffect.Update(gameTime);
            sprite.Update(gameTime);
            MouseState state = Mouse.GetState();
            sprite.Position = new(state.X, state.Y);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));
            _spriteBatch.Begin();
            sprite.Draw(_spriteBatch);
            _spriteBatch.Draw(stick, new Vector2(358, 180), Color.White);
            sparkleEmitter.Draw(_spriteBatch);
            fireSparkEmitter.Draw(_spriteBatch);
            smokeEmitter.Draw(_spriteBatch);
            fireRedEmitter.Draw(_spriteBatch);
            fireEmitter.Draw(_spriteBatch);
            fireCenterEmitter.Draw(_spriteBatch);
            fireEffect.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}