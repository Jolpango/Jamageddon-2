using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using Jamageddon2.Entities.Path;
using Jamageddon2.Entities.Enemies;

namespace Jamageddon2.Scenes
{
    public class PlayScene : JGameScene
    {
        public JSceneManager Parent { get; set; }
        private SpriteFont defaultFont;
        private UIButton endButton;
        private TowerSelector towerSelector;
        private UIButton startButton;
        private UIButton damageButton;
        private JTomatoEnemy tomatoEnemy;
        private string mapPath;
        public PlayScene(Game game, string mapPath, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
            this.mapPath = mapPath;
            defaultFont = game.Content.Load<SpriteFont>("Fonts/DefaultFont");
            RegisterService(defaultFont);
        }
        public override void LoadContent()
        {
            towerSelector = new TowerSelector();
            serviceInjector.Inject(towerSelector);
            towerSelector.LoadContent();

            SetPhysicsSystem(new JTopDownPhysicsSystem());
            defaultFont = game.Content.Load<SpriteFont>("Fonts/DefaultFont");
            TextElement sceneText = new TextElement()
            {
                Font = defaultFont,
                Text = "PlayScene",
                Position = new Vector2(600, 10),
                Color = Color.White,
            };
            endButton = new UIButton() { Size = new Vector2(32, 32), Position = new Vector2(10, 10) };
            endButton.OnClick += EndButton_OnClick;
            AddUIElement(endButton);
            AddUIElement(sceneText);

            // Start button
            startButton = new UIButton() { Size = new Vector2(32, 32), Position = new Vector2(50, 10) };
            startButton.OnClick += StartTomatoEnemy_OnClick;
            AddUIElement(startButton);

            // Damage button
            damageButton = new UIButton() { Size = new Vector2(32, 32), Position = new Vector2(100, 10) };
            damageButton.OnClick += DamageButton_OnClick;
            AddUIElement(damageButton);

            // Create path
            var path = new JPathComponent();
            path.SetSpawnPoint(new Vector2(0, 200));
            path.AddWaypoint(new Vector2(400, 200));
            path.AddWaypoint(new Vector2(400, 400));

            // Create tomato enemy
            tomatoEnemy = new JTomatoEnemy();
            tomatoEnemy.SetPath(path);
            AddEntity(tomatoEnemy);

            AddUIElement(towerSelector.RootElement);
            base.LoadContent();
            entityWorld.LoadMap(mapPath);
        }

        protected override void InjectAllServices()
        {
            serviceInjector.Inject(towerSelector);
            base.InjectAllServices();
        }

        private void EndButton_OnClick(UIButton obj)
        {
            endButton.OnClick -= EndButton_OnClick;
            Parent.PopScene();
        }

        private void StartTomatoEnemy_OnClick(UIButton obj)
        {
            tomatoEnemy.StartMovement();
        }

        private void DamageButton_OnClick(UIButton obj)
        {
            tomatoEnemy.TakeDamage(10);
        }
    }
}
