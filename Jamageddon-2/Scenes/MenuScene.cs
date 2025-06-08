using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using static Jamageddon2.JGameConstants;

namespace Jamageddon2.Scenes
{
    public class MenuScene : JGameScene
    {
        public JSceneManager Parent { get; set; }
        private SpriteFont defaultFont;
        public MenuScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
        }

        public override void LoadContent()
        {
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");

            // Create title text
            TextElement titleText = new TextElement()
            {
                Font = defaultFont,
                Text = "JAMAGEDDON II",
                Position = new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3),
                Color = Color.White,
            };
            titleText.LoadContent(); // Load content to get text size
            titleText.Position = new Vector2(
                (SCREEN_WIDTH - titleText.Size.X) / 2,
                SCREEN_HEIGHT / 3
            );

            // Create play button
            UIButton startButton = new UIButton()
            {
                Size = new Vector2(256, 64),
                Position = new Vector2(
                    (SCREEN_WIDTH - 256) / 2,
                    titleText.Position.Y + titleText.Size.Y + 50
                ),
                Text = "PLAY",
                Font = defaultFont,
                TextColor = Color.DarkSlateGray,
                BackgroundTexture = game.Content.Load<Texture2D>("Textures/buttonbackground"),
            };
            startButton.OnClick += StartButton_OnClick;
            startButton.OnMouseEnter += (UIElement e) =>
            {
                startButton.BorderColor = Color.Green;
                startButton.Text = "WAWAWEEWAA";
                startButton.BorderThickness = 2f;
            };
            startButton.OnMouseLeave += (UIElement e) =>
            {
                startButton.BorderColor = Color.Transparent;
                startButton.Text = "PLAY";
                startButton.BorderThickness = 0f;
            };

            AddUIElement(titleText);
            AddUIElement(startButton);
            base.LoadContent();
        }

        private void StartButton_OnClick(UIElement obj)
        {
            Parent.AddScene(new PlayScene(game, "Content/map-1.json") { Parent = Parent });
        }
    }
}
