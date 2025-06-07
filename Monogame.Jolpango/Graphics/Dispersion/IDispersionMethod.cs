using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Dispersion
{
    public interface IDispersionMethod
    {
        public void Update(GameTime gameTime, float easing, Particle particle);
    }
}
