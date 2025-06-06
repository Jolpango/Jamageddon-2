using Microsoft.Xna.Framework.Input;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS.Components
{
    public class JLeftMouseClickComponent : JComponent, IJInjectable<JMouseInput>
    {
        private JMouseInput mouseInput;
        public event Action<JLeftMouseClickComponent> OnClick;
        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }

        public override void LoadContent()
        {
            mouseInput.LeftButtonClicked += MouseInput_LeftButtonClicked;
            base.LoadContent();
        }

        private void MouseInput_LeftButtonClicked(MouseState mouseState)
        {
            if(!mouseInput.IsUIFocused)
            {
                OnClick?.Invoke(this);
            }
        }
    }
}
