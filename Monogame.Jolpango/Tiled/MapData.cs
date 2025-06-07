using System.Collections.Generic;

namespace MonoGame.Jolpango.Tiled
{
    public class MapData
    {
        public int CompressionLevel { get; set; }
        public int Height { get; set; }
        public bool Infinite { get; set; }
        public List<BaseLayer> Layers { get; set; }
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

        public List<TileLayer> TileLayers { get; set; }
        public List<ObjectLayer> ObjectLayers { get; set; }
    }
}
