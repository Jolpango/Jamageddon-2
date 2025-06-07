using Jamageddon2.Entities.Towers;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Graphics.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jamageddon2.UI
{
    public class TowerDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpritePath { get; set; }
        public int Cost { get; set; }

        public JBaseTower TowerToCreate { get; set; }
    }
}
