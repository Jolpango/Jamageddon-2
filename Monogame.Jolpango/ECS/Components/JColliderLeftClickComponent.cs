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
    public class JColliderLeftClickComponent : JComponent, IJInjectable<JMouseInput>
    {
        private JMouseInput mouseInput;
        public event Action<JEntity> OnClick;
        public void Inject(JMouseInput service)
        {
            mouseInput = service;
        }

        public override void Update(GameTime gameTime)
        {
            var collider = Parent.GetComponent<JColliderComponent>();
            if (collider is not null)
            {
                if(mouseInput.IsLeftButtonClicked() && collider.Contains(mouseInput.Position))
                {
                    OnClick?.Invoke(Parent);
                }
            }
            base.Update(gameTime);
        }
    }
}
