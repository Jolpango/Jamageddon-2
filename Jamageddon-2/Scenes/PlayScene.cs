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
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

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
        private JPathComponent path;
        private Player player;
        private UIStackPanel playerStatsPanel;
        private TextElement livesLeftText;
        private TextElement goldText;
        private JBaseTower selectedTower;


        private UIStackPanel selectedTowerContainer;
        private TextElement selectedTowerText;

        public PlayScene(Game game, string mapPath, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
            this.mapPath = mapPath;
            player = new Player();
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");

            // Create path
            path = new JPathComponent();
            path.SetSpawnPoint(new Vector2(260, 0));
            path.AddWaypoint(new Vector2(260, 140));
            path.AddWaypoint(new Vector2(0, 140));
            path.AddWaypoint(new Vector2(0, 400));
            path.AddWaypoint(new Vector2(400, 400));
            path.AddWaypoint(new Vector2(400, 0));
            path.AddWaypoint(new Vector2(500, 0));

            levelSpawner = new JLevelSpawner(game, this, path);
            RegisterService(player);
            RegisterService(defaultFont);
            RegisterService(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (mouseInput.IsRightButtonClicked() || keyboardInput.IsKeyPressed(Keys.Escape))
            {
                // Deselect tower
                SelectExistingTower(null);
            }
            levelSpawner.Update(gameTime);
            towerSelector.Update();
            livesLeftText.Text = "Lives left: " + player.LivesLeft;
            goldText.Text = "Gold: " + player.Gold;
        }

        public override void LoadContent()
        {
            towerSelector = new TowerSelector();
            serviceInjector.Inject(towerSelector);
            towerSelector.LoadContent();

            SetPhysicsSystem(new JTopDownPhysicsSystem());
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");
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
            startButton.OnClick += StartLevel_OnClick;
            AddUIElement(startButton);

            // Player stats
            playerStatsPanel = new UIStackPanel()
            {
                Gap = 20,
                Orientation = Orientation.Vertical,
                BackgroundColor = Color.CornflowerBlue,
                Padding = new Vector2(10),
            };
            livesLeftText = new TextElement() { Text = "Lives left: " + player.LivesLeft, Color = Color.Red, Font = defaultFont };
            goldText = new TextElement() { Text = "Gold: " + player.Gold, Color = Color.Gold, Font = defaultFont };
            playerStatsPanel.AddChild(goldText);
            playerStatsPanel.AddChild(livesLeftText);
            UIStackPanel rightSideUIPanel = new UIStackPanel()
            {
                Position = new Vector2(1000, 10),
                Orientation = Orientation.Horizontal,
            };
            rightSideUIPanel.AddChild(playerStatsPanel);
            rightSideUIPanel.AddChild(towerSelector.RootElement);
            AddUIElement(rightSideUIPanel);

            //Selected tower container
            
            selectedTowerContainer = new UIStackPanel()
            {
                Size = new Vector2(800, 200),
                MinSize = new Vector2(800, 200),
                Position = new Vector2(1280 / 2 - 400, 500),
                Orientation = Orientation.Vertical,
                BackgroundColor = Color.ForestGreen,
                AlignItems = ItemAlignment.Center,
                IsEnabled = false,
                IsVisible = false,
            };

            selectedTowerText = new TextElement()
            {
                Color = Color.Red,
                Text = "No selected tower",
                Font = defaultFont,
            };
            selectedTowerContainer.AddChild(selectedTowerText);
            AddUIElement(selectedTowerContainer);

            base.LoadContent();
            entityWorld.LoadMap(mapPath);
            RegisterService(Parent);

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

        public void SelectExistingTower(JBaseTower jBaseTower)
        {
            if(jBaseTower is null)
            {
                selectedTower = null;
                selectedTowerContainer.IsEnabled = false;
                selectedTowerContainer.IsVisible = false;
                return;
            }
            selectedTower = jBaseTower;
            selectedTowerContainer.IsEnabled = true;
            selectedTowerContainer.IsVisible = true;
            selectedTowerText.Text = jBaseTower.Name;
        }
    }
}
