using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Utilities;

namespace Jamageddon2.Entities.Components
{
    public class JHealthBarComponent : JComponent, IJInjectable<Game>
    {
        private JHealthComponent healthComponent;
        private JTransformComponent transformComponent;
        private Texture2D healthBarTexture;
        private Texture2D healthBarBackgroundTexture;
        public bool Enabled { get; set; } = true;
        public Vector2 Offset { get; set; } = new Vector2(0, 0);
        public Vector2 Size { get; set; } = new Vector2(32, 4);
        private Game game;

        public void Inject(Game service)
        {
            game = service;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            healthComponent = Parent.GetComponent<JHealthComponent>();
            transformComponent = Parent.GetComponent<JTransformComponent>();
            // Create textures for the health bar
            healthBarTexture = JTextureCache.White;
            healthBarBackgroundTexture = JTextureCache.White;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (healthComponent == null || transformComponent == null || !Enabled) return;

            Vector2 position = transformComponent.Position + Offset;
            float healthPercentage = healthComponent.CurrentHealth / healthComponent.MaxHealth;

            // Draw background
            spriteBatch.Draw(healthBarBackgroundTexture,
                new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y),
                Color.DarkGray);

            // Draw health bar
            Color color = Color.Green;
            if (healthPercentage < 0.9)
                color = Color.Orange;
            if (healthPercentage < 0.5)
                color = Color.Red;
            spriteBatch.Draw(healthBarTexture,
                new Rectangle((int)position.X, (int)position.Y, (int)(Size.X * healthPercentage), (int)Size.Y),
                color);
        }
    }
}