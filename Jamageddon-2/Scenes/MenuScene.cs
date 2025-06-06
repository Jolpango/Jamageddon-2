using Microsoft.Xna.Framework;
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
        public MenuScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
        }
        public override void LoadContent()
        {
            UIButton startButton = new UIButton() { Size = new Vector2(256, 64), Position = new Vector2(200, 200) };
            startButton.OnClick += StartButton_OnClick;
            AddUIElement(startButton);
            base.LoadContent();
        }

        private void StartButton_OnClick(UIButton obj)
        {
            Parent.AddScene(new PlayScene(game) { Parent = Parent});
        }
    }
}
