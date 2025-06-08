using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Projectiles;
using Jamageddon2.Scenes;
using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.UI.Elements;
using MonoGame.Jolpango.UI.Elements.Containers;
using MonoGame.Jolpango.Utilities;

namespace Jamageddon2.Entities.Towers
{
    public abstract class JBaseTower : JEntity, IJInjectable<ExistingTowerSelectedUI>, IJInjectable<SpriteFont>
    {
        public float Damage { get; protected set; }
        public float Range { get; protected set; }
        public float FireRate { get; protected set; }

        protected int currentWaypointIndex = 0;
        protected float waypointReachedDistance = 5f;

        protected ExistingTowerSelectedUI existingTowerSelectedUI;

        private SpriteFont defaultFont;

        public void Inject(ExistingTowerSelectedUI service)
        {
            existingTowerSelectedUI = service;
        }
        public void Inject(SpriteFont service)
        {
            defaultFont = service;
        }

        protected JBaseTower(string spritePath, float damage, float range, float fireRate)
        {
            Damage = damage;
            Range = range;
            FireRate = fireRate;

            AddComponent(new JSpriteComponent(spritePath));
            AddComponent(new JTransformComponent());
            AddComponent(new JTopDownPlayerInputComponent());
            AddComponent(new JBoxColliderComponent() { Size = Vector2.One, IsSolid = false });
            AddComponent(new JTargetEnemyComponent() { FireRate = fireRate });
            AddComponent(new JShootComponent());
            AddComponent(new JColliderLeftClickComponent());
            InitializeToolTip();
            GetComponent<JColliderLeftClickComponent>().OnClick += JBaseTower_OnClick;
        }

        private void InitializeToolTip()
        {
            var stackPanel = new UIStackPanel()
            {
                Orientation = Orientation.Vertical,
                Padding = new Vector2(10, 10),
                BackgroundColor = Color.White * 0.5f,
                BorderColor = Color.Black * 0.5f,
                BorderThickness = 1f,
                Gap = 5f,
                IsVisible = true,
                IsEnabled = true,
            };
            var nameTextElement = new TextElement()
            {
                Text = $"Name: {Name}",
                IsEnabled = true,
                IsVisible = true,
                Font = JFontCache.DefaultFont,
            };
            var damageTextElement = new TextElement()
            {
                Text = $"Damage: {Name}",
                IsEnabled = true,
                IsVisible = true,
                Font = JFontCache.DefaultFont,
            };
            var rangeTextElement = new TextElement()
            {
                Text = $"Range: {Range}",
                IsEnabled = true,
                IsVisible = true,
                Font = JFontCache.DefaultFont,
            };
            var fireRateTextElement = new TextElement()
            {
                Text = $"Fire Rate: {FireRate}",
                IsEnabled = true,
                IsVisible = true,
                Font = JFontCache.DefaultFont,
            };
            stackPanel.AddChild(nameTextElement);
            stackPanel.AddChild(damageTextElement);
            stackPanel.AddChild(rangeTextElement);
            stackPanel.AddChild(fireRateTextElement);
            AddComponent(new JTooltipComponent()
            {
                TooltipElement = stackPanel
            });
        }

        public override void Initialize()
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public abstract JBaseProjectile GetProjectile();
        private void JBaseTower_OnClick(JEntity obj)
        {
            existingTowerSelectedUI.SelectExistingTower(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
