using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JCircleColliderComponent : JColliderComponent
    {
        public float Radius { get; set; }
        public Vector2 Center => WorldPosition + new Vector2(Radius, Radius);

        private bool containsWithinRadius(Vector2 point, float radius) => Vector2.DistanceSquared(Center, point) <= radius * radius;

        public override bool Contains(Vector2 point) => containsWithinRadius(point, Radius);
        public override bool Intersects(JColliderComponent other) => other.IntersectsWith(this);
        public override bool IntersectsWith(JBoxColliderComponent box) => box.IntersectsWith(this);
        public override bool IntersectsWith(JCircleColliderComponent circle) => containsWithinRadius(circle.Center, Radius + circle.Radius);

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: This is drawing a rectangle and not a circle, I was lazy
            #if DEBUG
            spriteBatch.Draw(
                    JTextureCache.White,
                    WorldPosition,
                    new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Radius, (int)Radius),
                    new Color(Color.Blue, 69)
                );
            #endif
        }
    }
}