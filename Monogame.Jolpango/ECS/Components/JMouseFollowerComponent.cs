using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JMouseFollowerComponent : JComponent, IJInjectable<JMouseInput>
    {
        private JMouseInput mouseInput;
        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }
        public override void LoadContent()
        {
            mouseInput.MouseMoved += MouseInput_MouseMoved;
            base.LoadContent();
        }

        private void MouseInput_MouseMoved(Vector2 mousePosition)
        {
            var sprite = Parent.GetComponent<JSpriteComponent>().sprite;
            var size = new Vector2(sprite.spriteSheet.RegionWidth, sprite.spriteSheet.RegionHeight);
            Parent.GetComponent<JTransformComponent>().Position = mousePosition - (size / 2);
        }
    }
}
