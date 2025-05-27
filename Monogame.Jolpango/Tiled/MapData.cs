using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Tiled
{
    public class MapData
    {
        public MapData(Game game)
        {
        }

        public int CompressionLevel { get; set; }
        public int Height { get; set; }
        public bool Infinite { get; set; }
        public List<MapLayer> Layers { get; set; }
        public int NextLayerId { get; set; }
        public int NextObjectId { get; set; }
        public string Orientation { get; set; }
        public string RenderOrder { get; set; }
        public string TiledVersion { get; set; }
        public int TileHeight { get; set; }
        public List<TileSet> TileSets { get; set; }
        public int TileWidth { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public int Width { get; set; }

    }
}
