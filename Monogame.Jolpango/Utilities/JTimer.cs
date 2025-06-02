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
        private float iterationTime;
        private bool isRepeating = false;
        public bool Done { get { return time < 0; } }
        public JTimer(float time, Action callback, bool isRepeating = false)
        {
            this.time = time;
            this.iterationTime = time;
            this.callback = callback;
            this.isRepeating = isRepeating;
        }

        public void Update(GameTime gameTime)
        {
            time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time < 0)
            {
                if (isRepeating)
                {
                    time = iterationTime + time;
                }
                callback();
            }
        }
    }
}
