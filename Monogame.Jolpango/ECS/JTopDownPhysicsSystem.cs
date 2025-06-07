using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.ECS.Components;

namespace MonoGame.Jolpango.ECS
{
    public class JTopDownPhysicsSystem : JPhysicsSystem
    {
        public override void Update(GameTime gameTime)
        {
            List<JColliderComponent> enemies = colliders.Where(collider => collider.Parent.Tags.Contains("Enemy")).ToList();
            List<JColliderComponent> projectiles = colliders.Where(collider => collider.Parent.Tags.Contains("Projectile")).ToList();
            foreach (var projectile in projectiles)
            {
                foreach (var enemy in enemies)
                {
                    if (CheckCollision(enemy, projectile))
                    {
                        HandleCollision(enemy, projectile);
                        break;
                    }
                }
            }
        }

        protected override bool CheckCollision(JColliderComponent a,  JColliderComponent b)
        {
            if (a.Type == ColliderType.Box && b.Type == ColliderType.Box)
            {
                return (a.BoundingBox.Intersects(b.BoundingBox));
            }
            return false;
        }
        protected override void HandleCollision(JColliderComponent a, JColliderComponent b)
        {
            if (a.IsSolid && b.IsSolid)
            {
                ResolveCollision(a, b);
            }
            base.HandleCollision(a, b);
        }

        private void ResolveCollision(JColliderComponent a, JColliderComponent b)
        {
            var ta = a.Parent.GetComponent<JTransformComponent>();
            var tb = b.Parent.GetComponent<JTransformComponent>();

            var overlap = GetIntersectionDepth(a.BoundingBox, b.BoundingBox);

            if (Math.Abs(overlap.X) < Math.Abs(overlap.Y))
                ta.Position += new Vector2(overlap.X, 0);
            else
                ta.Position += new Vector2(0, overlap.Y);
        }

        // AABB intersection depth (minimum translation vector)
        private Vector2 GetIntersectionDepth(Rectangle a, Rectangle b)
        {
            float dx = (a.X + a.Width / 2f) - (b.X + b.Width / 2f);
            float dy = (a.Y + a.Height / 2f) - (b.Y + b.Height / 2f);

            float halfWidthSum = (a.Width + b.Width) / 2f;
            float halfHeightSum = (a.Height + b.Height) / 2f;

            float overlapX = halfWidthSum - Math.Abs(dx);
            float overlapY = halfHeightSum - Math.Abs(dy);

            if (overlapX > 0 && overlapY > 0)
            {
                return new Vector2(dx < 0 ? -overlapX : overlapX, dy < 0 ? -overlapY : overlapY);
            }

            return Vector2.Zero;
        }
    }
}
