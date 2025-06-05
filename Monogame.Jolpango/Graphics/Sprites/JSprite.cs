using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Content;
using MonoGame.Jolpango.Interfaces;
using System;
using System.Linq;

namespace MonoGame.Jolpango.Graphics.Sprites
{
    public class JSprite : IJDrawable, IJUpdatable
    {
        public float Alpha { get; set; } = 1.0f;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Color Color { get; set; } = Color.White;
        public float LayerDepth { get; set; } = 1.0f;
        public float Rotation { get; set; } = 0.0f;
        public SpriteEffects SpriteEffect { get; set; } = SpriteEffects.None;
        public Vector2 Origin { get => Vector2.Zero; }
        private JAnimation currentAnimation;
        private JAnimationSettings animationSettings;
        private JSpriteSheet spriteSheet;

        public JSprite()
        {
        }
        public void LoadContent(ContentManager content, string animationPath)
        {
            animationSettings = JSharedContent.Instance.GetAnimation(animationPath);
            spriteSheet = new JSpriteSheet(content.Load<Texture2D>(animationSettings.TextureAtlas.Texture),
                animationSettings.TextureAtlas.RegionWidth,
                animationSettings.TextureAtlas.RegionHeight);
            // Set start animation to first one. but dont play.
            currentAnimation = new JAnimation(animationSettings.Cycles.First().Value);
        }
        public void StartAnimation(string name, Action onComplete = null, bool loop = false)
        {
            // Find animation from animation settings
            var animationCycleSettings = animationSettings.Cycles[name];
            if (animationCycleSettings is null)
                return;
            currentAnimation = new JAnimation(animationCycleSettings);
            currentAnimation.StartAnimation(onComplete: onComplete, loop: loop);
        }
        public void UnloadContent() { }
        public void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet.Texture,
                Position,
                spriteSheet.GetRegion(currentAnimation.CurrentFrame).Region,
                Color * Alpha,
                Rotation,
                Origin,
                Scale,
                SpriteEffect,
                LayerDepth);
        }
    }
}
