using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;

namespace Jamageddon2.Scenes
{
    public class MenuScene : JGameScene
    {
        public JSceneManager Parent {  get; set; }
        private SpriteFont defaultFont;
        public MenuScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
        }
        public override void LoadContent()
        {
            defaultFont = game.Content.Load<SpriteFont>("Fonts/default");
            TextElement sceneText = new TextElement()
            {
                Font = defaultFont,
                Text = "MenuScene",
                Position = new Vector2(600, 10),
                Color = Color.White,
            };
            UIButton startButton = new UIButton() { Size = new Vector2(256, 64), Position = new Vector2(200, 200), BackgroundColor = Color.Turquoise };
            startButton.OnClick += StartButton_OnClick;
            AddUIElement(startButton);
            AddUIElement(sceneText);
            base.LoadContent();
        }

        private void StartButton_OnClick(UIElement obj)
        {
            Parent.AddScene(new PlayScene(game, "Content/map-1.json") { Parent = Parent});
        }
    }
}
