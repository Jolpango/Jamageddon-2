using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Graphics.Sprites;
using System;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JSpriteComponent : JComponent, IJInjectable<ContentManager>
    {
        private JSprite sprite = new JSprite();
        private string spritePath;
        private ContentManager contentManager;
        public JSpriteComponent(string spritePath)
        {
            this.spritePath = spritePath;
        }

        public void Inject(ContentManager service)
        {
            if (service is null)
                throw new ArgumentNullException(nameof(service));
            contentManager = service;
        }
        public override void LoadContent()
        {
            sprite.LoadContent(contentManager, spritePath);
            UpdateTransform();
        }

        public override void UnloadContent()
        {
            sprite.UnloadContent();
        }

        public void PlayAnimation(string name, bool loop = false, Action onComplete = null)
        {
            sprite.StartAnimation(name, onComplete, loop);
        }

        public void UpdateTransform()
        {
            var transform = Parent.GetComponent<JTransformComponent>();
            if (transform is not null)
            {
                sprite.Position = transform.Position;
                sprite.Scale = transform.Scale;
                sprite.Rotation = transform.Rotation;
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdateTransform();
            sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}
