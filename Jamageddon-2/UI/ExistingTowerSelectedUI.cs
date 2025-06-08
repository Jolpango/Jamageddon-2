using Jamageddon2.Entities.Components;
using Jamageddon2.Entities.Towers;
using Jamageddon2.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using MonoGame.Jolpango.UI.Elements.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2.UI
{
    public class ExistingTowerSelectedUI : IJInjectable<JMouseInput>, IJInjectable<SpriteFont>
    {
        private bool towerSelectedThisFrame = false;
        private JBaseTower selectedTower = null;
        private UIStackPanel selectedTowerContainer;
        private TextElement selectedTowerText;
        private List<UIButton> targetModeButtons = new List<UIButton>();

        private UIStackPanel selectedTowerStatsContainer;
        private TextElement selectedTowerStatsTextRange;
        private TextElement selectedTowerStatsTextDamage;
        private TextElement selectedTowerStatsTextFireRate;
        private List<TextElement> targetModeStatsText = new List<TextElement>();

        private JMouseInput mouseInput;
        private SpriteFont defaultFont;
        public UIElement RootElement { get => selectedTowerContainer; }
        public UIElement RootElementStats { get => selectedTowerStatsContainer; }
        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }
        public void Inject(SpriteFont service)
        {
            defaultFont = service;
        }
        public void LoadContent()
        {
            selectedTowerContainer = new UIStackPanel()
            {
                Orientation = Orientation.Vertical,
                Size = new Vector2(800, 200),
                MinSize = new Vector2(800, 200),
                Position = new Vector2(1280 / 2 - 400, 720 - 200 - 10),
                Padding = new Vector2(10),
                BackgroundColor = Color.Gray,
                AlignItems = ItemAlignment.Center,
                IsEnabled = false,
                IsVisible = false
            };
            selectedTowerText = new TextElement()
            {
                Text = "No tower selected",
                Color = Color.White,
                Font = defaultFont
            };
            var targetModeContainer = new UIStackPanel()
            {
                Orientation = Orientation.Horizontal,
                BackgroundColor = Color.LightGray,
                Padding = new Vector2(5),
                Gap = 10,
            };
            var closestButton = new UIButton()
            {
                Size = new Vector2(128, 32),
                Text = "Closest",
                Font = defaultFont
            };
            var furthestButton = new UIButton()
            {
                Size = new Vector2(128, 32),
                Text = "Furthest",
                Font = defaultFont
            };
            var toughestButton = new UIButton()
            {
                Size = new Vector2(128, 32),
                Text = "Toughest",
                Font = defaultFont
            };

            selectedTowerStatsContainer = new UIStackPanel()
            {
                Orientation = Orientation.Vertical,
                Size = new Vector2(400, 100),
                MinSize = new Vector2(400, 100),
                Padding = new Vector2(10),
                BackgroundColor = Color.Gray,
                AlignItems = ItemAlignment.Left,
            };
            selectedTowerStatsTextRange = new TextElement()
            {
                Text = "TOWER RANGE",
                Color = Color.White,
                Font = defaultFont
            };
            selectedTowerStatsTextDamage = new TextElement()
            {
                Text = "TOWER DAMAGE",
                Color = Color.White,
                Font = defaultFont
            };
            selectedTowerStatsTextFireRate = new TextElement()
            {
                Text = "TOWER FireRate",
                Color = Color.White,
                Font = defaultFont
            };


            targetModeStatsText.Add(selectedTowerStatsTextDamage);
            targetModeStatsText.Add(selectedTowerStatsTextRange);
            targetModeStatsText.Add(selectedTowerStatsTextFireRate);
            selectedTowerStatsContainer.AddChild(selectedTowerStatsTextDamage);
            selectedTowerStatsContainer.AddChild(selectedTowerStatsTextRange);
            selectedTowerStatsContainer.AddChild(selectedTowerStatsTextFireRate);


            closestButton.OnClick += (UIElement e) => SetTargetMode(TargetingMode.Closest);
            closestButton.OnMouseEnter += OnMouseEnter;
            closestButton.OnMouseLeave += OnMouseLeave;


            furthestButton.OnClick += (UIElement e) => SetTargetMode(TargetingMode.Farthest);
            furthestButton.OnMouseEnter += OnMouseEnter;
            furthestButton.OnMouseLeave += OnMouseLeave;

            toughestButton.OnClick += (UIElement e) => SetTargetMode(TargetingMode.Toughest);
            toughestButton.OnMouseEnter += OnMouseEnter;
            toughestButton.OnMouseLeave += OnMouseLeave;

            selectedTowerContainer.AddChild(selectedTowerText);

            targetModeContainer.AddChild(closestButton);
            targetModeContainer.AddChild(furthestButton);
            targetModeContainer.AddChild(toughestButton);
            targetModeButtons.Add(closestButton);
            targetModeButtons.Add(furthestButton);
            targetModeButtons.Add(toughestButton);
            selectedTowerContainer.AddChild(targetModeContainer);
            selectedTowerContainer.AddChild(selectedTowerStatsContainer);
        }

        private void OnMouseEnter(UIElement e)
        {
            e.BorderColor = Color.Black;
            e.BorderThickness = 1f;
        }

        private void OnMouseLeave(UIElement e)
        {
            e.BorderColor = Color.Transparent;
            e.BorderThickness = 0f;
        }

        private void SetTargetMode(TargetingMode targetingMode)
        {
            selectedTower.GetComponent<JTargetEnemyComponent>().TargetingMode = targetingMode;
        }

        public void Update()
        {
            if (mouseInput.IsLeftButtonClicked() && !towerSelectedThisFrame && !mouseInput.IsUIFocused)
            {
                SelectExistingTower(null);
            }
            if (selectedTower is not null)
            {
                var currentTargetMode = selectedTower.GetComponent<JTargetEnemyComponent>().TargetingMode;
                foreach (var button in targetModeButtons)
                {
                    if (button.Text == "Closest")
                    {
                        button.BackgroundColor = selectedTower?.GetComponent<JTargetEnemyComponent>().TargetingMode == TargetingMode.Closest ? Color.Green : Color.White;
                    }
                    else if (button.Text == "Furthest")
                    {
                        button.BackgroundColor = selectedTower?.GetComponent<JTargetEnemyComponent>().TargetingMode == TargetingMode.Farthest ? Color.Green : Color.White;
                    }
                    else if (button.Text == "Toughest")
                    {
                        button.BackgroundColor = selectedTower?.GetComponent<JTargetEnemyComponent>().TargetingMode == TargetingMode.Toughest ? Color.Green : Color.White;
                    }
                }
            }
            towerSelectedThisFrame = false;
        }

        public void SelectExistingTower(JBaseTower jBaseTower)
        {
            if (jBaseTower is null)
            {
                selectedTower = null;
                selectedTowerContainer.IsEnabled = false;
                selectedTowerContainer.IsVisible = false;

                return;
            }
            towerSelectedThisFrame = true;
            selectedTower = jBaseTower;
            selectedTowerContainer.IsEnabled = true;
            selectedTowerContainer.IsVisible = true;
            selectedTowerText.Text = jBaseTower.Name;
        
            selectedTowerStatsTextDamage.Text = $"Damage: {jBaseTower.Damage}";
            selectedTowerStatsTextRange.Text = $"Range: {jBaseTower.Range}";
            selectedTowerStatsTextFireRate.Text = $"FireRate: {1/jBaseTower.FireRate} /s";
        }
    }
}
