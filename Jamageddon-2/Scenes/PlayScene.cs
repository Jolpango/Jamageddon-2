using Jamageddon2.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Level;

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
        public PlayScene(Game game, string mapPath, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
            this.mapPath = mapPath;
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");

            // Create path
            path = new JPathComponent();
            path.SetSpawnPoint(new Vector2(130, 0));
            path.AddWaypoint(new Vector2(130, 70));
            path.AddWaypoint(new Vector2(0, 70));
            path.AddWaypoint(new Vector2(0, 200));
            path.AddWaypoint(new Vector2(200, 200));
            path.AddWaypoint(new Vector2(200, 0));
            path.AddWaypoint(new Vector2(250, 0));

            levelSpawner = new JLevelSpawner(game, this, path);
            RegisterService(defaultFont);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            levelSpawner.Update(gameTime);
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

        private void StartLevel_OnClick(UIButton obj)
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
