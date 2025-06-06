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
            Parent.GetComponent<JTransformComponent>().Position = mousePosition;
        }
    }
}
