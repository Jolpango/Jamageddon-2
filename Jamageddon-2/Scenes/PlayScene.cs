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
    public class PlayScene : JGameScene
    {
        public JSceneManager Parent { get; set; }
        private UIButton endButton;
        public PlayScene(Game game, JMouseInput mouseInput = null, JKeyboardInput keyboardInput = null) : base(game, mouseInput, keyboardInput)
        {
        }
        public override void LoadContent()
        {
            endButton = new UIButton() { Size = new Vector2(32, 32), Position = new Vector2(10, 10) };
            endButton.OnClick += EndButton_OnClick;
            AddUIElement(endButton);
            base.LoadContent();
        }

        private void EndButton_OnClick(UIButton obj)
        {
            endButton.OnClick -= EndButton_OnClick;
            Parent.PopScene();
        }
    }
}
