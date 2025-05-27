using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Components
{
    public abstract class JComponent
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<string> Tags { get; set; }
        public Game Game { get; private set; }
        public JComponent(Game game)
        {
            Game = game;
            Id = Guid.NewGuid().ToString();
            Tags = new List<string>();
        }

        public void LoadContent()
        {

        }
        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
