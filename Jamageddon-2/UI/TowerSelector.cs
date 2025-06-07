using Jamageddon2.Entities.Components;
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Jamageddon2.JGameConstants;

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
                Position = new Vector2(1280 - 64 - 60, 30),
                Padding = new Vector2(10),
                BackgroundColor = Color.DarkGray,
                AlignItems = ItemAlignment.Center,
            };
            var text = new TextElement()
            {
                Text = "Towers",
                Color = Color.White,
                Font = font
            };
            RootElement.AddChild(text);
            UIStackPanel outerTowerContainer = new UIStackPanel()
            {
                Gap = 10,
                Orientation = Orientation.Horizontal,
                BackgroundColor = Color.Gray,
                Padding = new Vector2(5),
                MinSize = new Vector2(0, 500)
            };
            UIStackPanel column1 = new UIStackPanel()
            {
                Gap = 10,
                Orientation = Orientation.Vertical
            };
            UIStackPanel column2 = new UIStackPanel()
            {
                Gap = 10,
                Orientation = Orientation.Vertical
            };

            column1.AddChild(CreateTowerButton("JDishWasherTower", "Freaking interns man", 30, "Content/Animation/busboy.json", new JDishWasherTower()));
            column2.AddChild(CreateTowerButton(
                "JButcherChefTower",
                "The only thing rarer than his steaks are survivors.",
                40,
                "Content/Animation/butcherChef.json",
                new JButcherChefTower()
            ));

            outerTowerContainer.AddChild(column1);
            outerTowerContainer.AddChild(column2);
            RootElement.AddChild(outerTowerContainer);
            RootElement.LoadContent();
        }

        private TowerButton CreateTowerButton(string towerType, string description, int cost, string spritePath, JBaseTower towerToCreate)
        {
            TowerButton button = new TowerButton()
            {
                Size = new Vector2(32, 32),
                TowerDefinition = new TowerDefinition()
                {
                    Name = towerType,
                    Description = description,
                    Cost = cost,
                    SpritePath = spritePath,
                    TowerToCreate = towerToCreate,
                    Footprint = new Footprint()
                    {
                        Size = new Vector2(SMALL_TOWER_SIZE, SMALL_TOWER_SIZE),
                        Offset = new Vector2(SMALL_TOWER_SIZE / 2, SMALL_TOWER_SIZE)
                    }
                }
            };
            button.BackgroundColor = Color.White;
            buttons.Add(button);
            button.OnClick += OnSelectTower;
            return button;
        }

        public void Update()
        {
            foreach (TowerButton button in buttons)
            {
                button.BackgroundColor = Color.White;
                if (button.TowerDefinition.Cost > player.Gold)
                {
                    button.BackgroundColor = Color.Red;
                }
            }
        }

        private void OnSelectTower(UIElement obj)
        {
            if (selectedTower is not null)
                return;

            if (obj is TowerButton towerButton)
            {
                if (towerButton.TowerDefinition.Cost > player.Gold)
                    return;
                selectedTower = towerButton.TowerDefinition;

                TowerPlacer towerPlacer = new TowerPlacer(selectedTower.SpritePath, selectedTower);
                towerPlacer.AddComponent(new JRangeIndicatorComponent()
                {
                    Color = new Color(0, 50, 0, 80),
                    Range = towerButton.TowerDefinition.TowerToCreate.Range
                });
                towerPlacer.Name = towerButton.TowerDefinition.Name;
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
