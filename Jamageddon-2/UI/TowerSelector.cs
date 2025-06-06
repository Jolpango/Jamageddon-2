using Jamageddon2.Entities.Enemies;
using Jamageddon2.Towers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.UI.Elements;
using MonoGame.Jolpango.UI.Elements.Containers;
using System.Diagnostics;

namespace Jamageddon2.UI
{
    public class TowerSelector : IJInjectable<ContentManager>, IJInjectable<SpriteFont>, IJInjectable<JGameScene>
    {
        private ContentManager contentManager;
        public UIStackPanel RootElement;
        private SpriteFont font;
        private TowerDefinition selectedTower;
        private JGameScene gameScene;
        public TowerSelector() { }

        public void Inject(ContentManager service)
        {
            contentManager = service;
        }
        public void Inject(SpriteFont service)
        {
            font = service;
        }

        public void Inject(JGameScene service)
        {
            gameScene = service;
        }
        public void LoadContent()
        {
            RootElement = new UIStackPanel()
            {
                Orientation = Orientation.Vertical,
                Position = new Vector2(1280 - 64 - 20, 10)
            };
            var text = new TextElement()
            {
                Text = "Towers",
                Color = Color.White,
                Font = font
            };
            RootElement.AddChild(text);
            UIStackPanel outerTowerContainer = new UIStackPanel() { Gap = 10, Orientation = Orientation.Horizontal };
            for(int i = 0; i < 2; i++)
            {
                UIStackPanel innerTowerContainer = new UIStackPanel() { Gap = 10, Orientation = Orientation.Vertical };
                for(int j = 0; j < 10; j++)
                {
                    TowerButton button = new TowerButton() {
                        Size = new Vector2(32, 32),
                        TowerDefinition = new TowerDefinition() { Name = $"Tower[{i}, {j}]" }
                    };
                    button.OnClick += OnSelectTower;
                    innerTowerContainer.AddChild(button);
                }
                outerTowerContainer.AddChild(innerTowerContainer);
            }
            RootElement.LoadContent();
            RootElement.AddChild(outerTowerContainer);
        }

        private void OnSelectTower(UIButton obj)
        {
            if (obj is TowerButton towerButton)
            {
                selectedTower = towerButton.TowerDefinition;
                TowerPlacer towerPlacer = new TowerPlacer("Content/Animation/axe.json");
                towerPlacer.TowerDefinition = selectedTower;
                towerPlacer.GetComponent<JLeftMouseClickComponent>().OnClick += OnPlaceTower;
                gameScene.AddEntity(towerPlacer);
                Debug.WriteLine("Selected tower: " +  selectedTower.Name);
            }
        }

        private void OnPlaceTower(JLeftMouseClickComponent obj)
        {
            if (obj.Parent is TowerPlacer towerPlacer)
            {
                towerPlacer.GetComponent<JLeftMouseClickComponent>().OnClick -= OnPlaceTower;
                Debug.WriteLine("Placing tower: " + towerPlacer.TowerDefinition.Name);
                JTomatoEnemy jTomatoEnemy = new JTomatoEnemy();
                gameScene.AddEntity(jTomatoEnemy);
                obj.Parent.DestroyEntity();
            }
        }
    }
}
