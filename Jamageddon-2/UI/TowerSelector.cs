using Jamageddon2.Entities.Enemies;
using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.UI.Elements;
using MonoGame.Jolpango.UI.Elements.Containers;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jamageddon2.UI
{
    public class TowerSelector : IJInjectable<ContentManager>, IJInjectable<SpriteFont>, IJInjectable<JGameScene>, IJInjectable<Player>
    {
        private ContentManager contentManager;
        public UIStackPanel RootElement;
        private SpriteFont font;
        private TowerDefinition selectedTower;
        private JGameScene gameScene;
        private Player player;
        private List<TowerButton> buttons = new();
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
        public void Inject(Player service)
        {
            player = service;
        }
        public void LoadContent()
        {
            RootElement = new UIStackPanel()
            {
                Orientation = Orientation.Vertical,
                Position = new Vector2(1280 - 64 - 20, 30)
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
                        TowerDefinition = new TowerDefinition() { Name = "JDishWasherTower", Description = "Freaking interns man", Cost = 30 }
                    };
                    buttons.Add(button);
                    button.OnClick += OnSelectTower;
                    innerTowerContainer.AddChild(button);
                }
                outerTowerContainer.AddChild(innerTowerContainer);
            }
            RootElement.LoadContent();
            RootElement.AddChild(outerTowerContainer);
        }

        public void Update()
        {
            foreach(TowerButton button in buttons)
            {
                button.Color = Color.White;
                if (button.TowerDefinition.Cost > player.Gold)
                {
                    button.Color = Color.Red;
                }
            }
        }

        private void OnSelectTower(UIButton obj)
        {
            if (selectedTower is not null)
                return;

            if (obj is TowerButton towerButton)
            {
                if (towerButton.TowerDefinition.Cost > player.Gold)
                    return;
                selectedTower = towerButton.TowerDefinition;
                TowerPlacer towerPlacer = new TowerPlacer("Content/Animation/busboy.json");
                towerPlacer.Name = towerButton.TowerDefinition.Name;
                towerPlacer.TowerDefinition = towerButton.TowerDefinition;
                towerPlacer.OnDestroy += TowerPlacer_OnDestroy;
                gameScene.AddEntity(towerPlacer);
            }
        }

        private void TowerPlacer_OnDestroy(JEntity obj)
        {
            selectedTower = null;
        }
    }
}
