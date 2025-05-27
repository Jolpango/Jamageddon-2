using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Tiled
{
    public class MapLayer
    {

        public List<int> Data { get; set; }
        public int Height { get; set; }
        public double Opacity { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
