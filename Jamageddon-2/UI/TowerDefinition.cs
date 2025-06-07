using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Jamageddon2.UI
{
    public class Footprint
    {
        public Vector2 Size { get; set; } = Vector2.One;
        public Vector2 Offset { get; set; } = Vector2.Zero;
    }

    public class TowerDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpritePath { get; set; }
        public int Cost { get; set; }

        public Footprint Footprint { get; set; }

        public void LoadContent()
        {
            
        }
        public JBaseTower TowerToCreate { get; set; }
    }
}
