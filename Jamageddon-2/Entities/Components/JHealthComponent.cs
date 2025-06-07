using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS.Components;

namespace Jamageddon2.Entities.Components
{
    public class JHealthComponent : JComponent, IJInjectable<Player>
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsAlive => CurrentHealth > 0;

        public event System.Action<float> OnHealthChanged;
        public event System.Action OnDeath;

        private Player player;

        public void Inject(Player service)
        {
            player = service;
        }

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
                player.AddGold(1);
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