using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;

namespace Jamageddon2.Entities.Components
{
    public class JTooltipComponent : JComponent, IJInjectable<JMouseInput>
    {
        private JMouseInput mouseInput;
        public UIElement TooltipElement { get; set; }

        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }

        public override void LoadContent()
        {
            if (TooltipElement is null)
            {
                TooltipElement.LoadContent();
            }
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            var colliderComponent = Parent.GetComponent<JColliderComponent>();
            var transformComponent = Parent.GetComponent<JTransformComponent>();
            if (colliderComponent is not null && transformComponent is not null)
            {
                var collisionBox = new Rectangle(
                    (int)transformComponent.Position.X + (int)colliderComponent.Offset.X,
                    (int)transformComponent.Position.Y + (int)colliderComponent.Offset.Y,
                    (int)colliderComponent.Size.X,
                    (int)colliderComponent.Size.Y
                );
                if(collisionBox.Contains(mouseInput.Position.ToPoint()))
                {
                    if (TooltipElement is not null && !TooltipElement.IsVisible)
                    {
                        TooltipElement.IsVisible = true;
                    }
                }
                else
                {
                    if (TooltipElement is not null && TooltipElement.IsVisible)
                    {
                        TooltipElement.IsVisible = false;
                    }
                }
            }
            if (TooltipElement is not null && mouseInput is not null)
            {
                TooltipElement.Update(gameTime, null, null);
                TooltipElement.Position = mouseInput.Position - new Vector2(TooltipElement.Size.X / 2f, TooltipElement.Size.Y);
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (TooltipElement is not null)
            {
                TooltipElement.Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }
    }
}