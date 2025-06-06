using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JHealthComponent : JComponent
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsAlive => CurrentHealth > 0;

        public event System.Action<float> OnHealthChanged;
        public event System.Action OnDeath;

        public override void LoadContent()
        {
            base.LoadContent();
            MaxHealth = MaxHealth;
            CurrentHealth = MaxHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            if (!IsAlive) return;

            float previousHealth = CurrentHealth;
            CurrentHealth = System.Math.Max(0, CurrentHealth - damage);

            OnHealthChanged?.Invoke(CurrentHealth);

            if (!IsAlive)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            float previousHealth = CurrentHealth;
            CurrentHealth = System.Math.Min(MaxHealth, CurrentHealth + amount);

            OnHealthChanged?.Invoke(CurrentHealth);
        }

        public override void Update(GameTime gameTime)
        {
            // Health component doesn't need per-frame updates
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Health component doesn't need to draw anything
        }
    }
}