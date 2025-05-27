using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Utilities
{
    public delegate void MonoTimerCallback();
    // UpdateSharedTimers needs to be called for shared timers to work
    public class JTimer
    {

        private Action callback;
        private float time;
        public bool Done { get { return time < 0; } }
        public JTimer(float time, Action callback)
        {
            this.time = time;
            this.callback = callback;
        }

        public void Update(GameTime gameTime)
        {
            time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time < 0)
            {
                callback();
            }
        }
    }
}
