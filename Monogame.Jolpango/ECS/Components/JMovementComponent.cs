using Microsoft.Xna.Framework;

namespace MonoGame.Jolpango.ECS.Components
{
    /// <summary>
    /// Requires a JInputComponent and JTransformComponent
    /// </summary>
    public class JMovementComponent : JComponent
    {
        public float Speed { get; set; } = 1;
        public override void Update(GameTime gameTime)
        {
            var input = Parent.GetComponent<JInputComponent>();
            var transform = Parent.GetComponent<JTransformComponent>();

            if(input is not null && transform is not null)
            {
                var intent = input.MoveIntent;
                if (intent != Vector2.Zero)
                {
                    intent.Normalize();
                    transform.Position += intent * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }
    }
}
