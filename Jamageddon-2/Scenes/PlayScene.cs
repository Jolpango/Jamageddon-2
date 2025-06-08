using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Level;
using MonoGame.Jolpango.UI.Elements.Containers;
using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using MonoGame.Jolpango.UI;

namespace Jamageddon2.Scenes
{
    public class PlayScene : JGameScene
    {
        public JSceneManager Parent { get; set; }
        private SpriteFont defaultFont;
        private UIButton endButton;
        private TowerSelector towerSelector;
        private UIButton startButton;
        private string mapPath;
        private JLevelSpawner levelSpawner;
        private Player player;
        private UIStackPanel playerStatsPanel;
        private TextElement livesLeftText;
        private TextElement goldText;
        private JBaseTower selectedTower;
        private ExistingTowerSelectedUI selectedTowerContainer;


        public PlayScene(Game game, string mapPath, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
            this.mapPath = mapPath;
            player = new Player();
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");
            selectedTowerContainer = new ExistingTowerSelectedUI();

            RegisterService(selectedTowerContainer);
            RegisterService(player);
            RegisterService(defaultFont);
            RegisterService(this);
        }

        public override void Update(GameTime gameTime)
        {
            towerSelector.Update();
            selectedTowerContainer.Update();
            livesLeftText.Text = "Lives left: " + player.LivesLeft;
            goldText.Text = "Gold: " + player.Gold;
            base.Update(gameTime);
        }

        public override void LoadContent()
        {
            RegisterService(Parent);

            towerSelector = new TowerSelector();
            serviceInjector.Inject(towerSelector);
            serviceInjector.Inject(selectedTowerContainer);
            towerSelector.LoadContent();
            selectedTowerContainer.LoadContent();
            SetPhysicsSystem(new JTopDownPhysicsSystem());
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");


            // UI Frame
            UIStackPanel topPanel = new UIStackPanel()
            {
                Position = new Vector2(0, 0),
                MinSize = new Vector2(1280, 50),
                BackgroundColor = Color.SaddleBrown,
                Orientation = Orientation.Horizontal,
                AlignItems = ItemAlignment.Center,
                Padding = new Vector2(10, 0),
                Gap = 30,
            };
            
            AddUIElement(topPanel);

            UIStackPanel rightPanel = new UIStackPanel()
            {
                Position = new Vector2(1280 - 200, 0),
                MinSize = new Vector2(200, 720),
                Padding = new Vector2(0, 50),
                Gap = 10,
                BackgroundColor = Color.SaddleBrown,
                Orientation = Orientation.Vertical,
                AlignItems = ItemAlignment.Center,
            };
            AddUIElement(rightPanel);

            // End button
            endButton = new UIButton()
            {
                Size = new Vector2(32, 32),
                Position = new Vector2(10, 10),
                Text = "X", Font = defaultFont,
                BackgroundColor = Color.Tomato
            };
            endButton.OnClick += EndButton_OnClick;
            endButton.OnMouseEnter += (UIElement e) =>
            {
                endButton.BorderColor = Color.Red;
                endButton.BorderThickness = 2f;
            };
            endButton.OnMouseLeave += (UIElement e) =>
            {
                endButton.BorderColor = Color.Transparent;
                endButton.BorderThickness = 0f;
            };
            topPanel.AddChild(endButton);

            // Start button
            startButton = new UIButton()
            {
                Size = new Vector2(120, 32),
                Position = new Vector2(50, 10),
                Text = "Start",
                Font = defaultFont,
                BackgroundColor = Color.LightGreen
            };
            startButton.OnMouseEnter += (UIElement e) =>
            {
                startButton.BorderColor = Color.Green;
                startButton.BorderThickness = 2f;
            };
            startButton.OnMouseLeave += (UIElement e) =>
            {
                startButton.BorderColor = Color.Transparent;
                startButton.BorderThickness = 0f;
            };
            startButton.OnClick += StartLevel_OnClick;

            livesLeftText = new TextElement() { Text = "Lives left: " + player.LivesLeft, Color = Color.Red, Font = defaultFont };
            goldText = new TextElement() { Text = "Gold: " + player.Gold, Color = Color.Gold, Font = defaultFont };
            topPanel.AddChild(goldText);
            topPanel.AddChild(livesLeftText);

            rightPanel.AddChild(towerSelector.RootElement);
            rightPanel.AddChild(startButton);
            //Selected tower container
            AddUIElement(selectedTowerContainer.RootElement);
            AddUIElement(selectedTowerContainer.RootElementStats);

            base.LoadContent();
            entityWorld.LoadMap(mapPath);

            JPathComponent path = new JPathComponent()
            {
                SpawnPoint = entityWorld.tileManager.path[0],
                Waypoints = entityWorld.tileManager.path.Skip(1).ToList()
            };
            levelSpawner = new JLevelSpawner(game, this, path);


        }

        protected override void InjectAllServices()
        {
            serviceInjector.Inject(towerSelector);
            base.InjectAllServices();
        }

        private void EndButton_OnClick(UIElement obj)
        {
            endButton.OnClick -= EndButton_OnClick;
            Parent.PopScene();
        }

        private void StartLevel_OnClick(UIElement obj)
        {
            if (!levelSpawner.IsLastLevel)
            {
                levelSpawner.StartLevel(levelSpawner.NextLevel);
            }
            else if (levelSpawner.IsAllEnemiesDestroyed)
            {
                EndButton_OnClick(endButton);
            }
            
        }
    }
}
