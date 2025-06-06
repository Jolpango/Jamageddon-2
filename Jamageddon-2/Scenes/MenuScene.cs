using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.Input;
using MonoGame.Jolpango.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            defaultFont = game.Content.Load<SpriteFont>("Fonts/DefaultFont");
            TextElement sceneText = new TextElement()
            {
                Font = defaultFont,
                Text = "MenuScene",
                Position = new Vector2(600, 10),
                Color = Color.White,
            };
            UIButton startButton = new UIButton() { Size = new Vector2(256, 64), Position = new Vector2(200, 200) };
            startButton.OnClick += StartButton_OnClick;
            AddUIElement(startButton);
            AddUIElement(sceneText);
            base.LoadContent();
        }

        private void StartButton_OnClick(UIButton obj)
        {
            Parent.AddScene(new PlayScene(game) { Parent = Parent});
        }
    }
}
