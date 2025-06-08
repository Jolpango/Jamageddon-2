
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.ECS.Components
{
    public abstract class JColliderComponent : JComponent
    {
        public event Action<JColliderComponent, JColliderComponent> OnCollision;
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

        public void TriggerCollision(JColliderComponent other) => OnCollision?.Invoke(this, other);

        public abstract bool Contains(Vector2 point);
        public abstract bool Intersects(JColliderComponent other);
        public abstract bool IntersectsWith(JBoxColliderComponent box);
        public abstract bool IntersectsWith(JCircleColliderComponent circle);
    }
}
