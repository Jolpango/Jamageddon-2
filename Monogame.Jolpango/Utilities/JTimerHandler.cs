using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Utilities
{
    public class JTimerHandler : GameComponent
    {
        private List<JTimer> timers = new List<JTimer>();
        public JTimerHandler(Game game) : base(game)
        {
        }

        public JTimer AddTimer(float time, Action action)
        {
            return AddTimer(new JTimer(time, action));
        }
        public JTimer AddTimer(JTimer timer)
        {
            timers.Add(timer);
            return timer;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timers.RemoveAll((timer) =>
            {
                timer.Update(gameTime);
                return timer.Done;
            });
        }
    }
}
