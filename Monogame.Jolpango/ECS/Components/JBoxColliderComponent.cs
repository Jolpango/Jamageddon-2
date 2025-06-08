using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JBoxColliderComponent : JColliderComponent
    {
        public Rectangle BoundingBox
        {
            get
            {
                var transform = Parent.GetComponent<JTransformComponent>();
                if (transform is null)
                    throw new Exception("BoxCollider requires TransformComponent");

                var pos = WorldPosition;
                var size = Size * transform.Scale;
                return new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
            }
        }

        public override bool Contains(Vector2 point) => BoundingBox.Contains(point);
        public override bool Intersects(JColliderComponent other) => other.IntersectsWith(this);
        public override bool IntersectsWith(JBoxColliderComponent box) => BoundingBox.Intersects(box.BoundingBox);
        public override bool IntersectsWith(JCircleColliderComponent circle)
        {
            Vector2 center = circle.Center;
            Rectangle box = BoundingBox;

            Vector2 closest = new Vector2(
                Math.Clamp(center.X, box.Left, box.Right),
                Math.Clamp(center.Y, box.Top, box.Bottom)
            );

            return Vector2.DistanceSquared(center, closest) <= circle.Radius * circle.Radius;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            #if DEBUG
                spriteBatch.Draw(JTextureCache.White, WorldPosition, BoundingBox, new Color(Color.Blue, 69));
            #endif
        }
    }
}