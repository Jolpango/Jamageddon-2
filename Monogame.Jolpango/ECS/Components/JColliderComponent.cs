
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.ECS.Components
{
    public enum ColliderType { Box, Circle }
    public class JColliderComponent : JComponent
    {
        public event Action<JColliderComponent, JColliderComponent> OnCollision;
        public ColliderType Type { get; set; }
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = Vector2.One;
        public bool IsSolid { get; set; } = false;

        public bool Centered { get; set; } = false;
        public Vector2 WorldPosition
        {
            get
            {
                var transform = Parent.GetComponent<JTransformComponent>();
                if (transform is null)
                {
                    throw new Exception("Collider requires TransfromComponent on the same Entity");
                }
                if (Centered)
                {
                    var sprite = Parent.GetComponent<JSpriteComponent>().sprite;
                    Offset = new Vector2(
                        (sprite.spriteSheet.RegionWidth - Size.X) / 2 * transform.Scale.X,
                        (sprite.spriteSheet.RegionHeight - Size.Y) / 2 * transform.Scale.Y);
                }
                return transform.Position + Offset;

            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                var transform = Parent.GetComponent<JTransformComponent>();
                if (transform is null)
                {
                    throw new Exception("Collider requires TransfromComponent on the same Entity");
                }
                var pos = WorldPosition;
                return new Rectangle(
                    (int)pos.X,
                    (int)pos.Y,
                    (int)(Size.X * transform.Scale.X),
                    (int)(Size.Y * transform.Scale.Y));
            }
        }

        public void TriggerCollision(JColliderComponent other)
        {
            OnCollision?.Invoke(this, other);
        }
    }
}
