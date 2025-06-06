using Microsoft.Xna.Framework;

namespace MonoGame.Jolpango.Tiled
{
    public class MapTile
    {
        public int TileIndex { get; set; }
        public Vector2 Position { get; set; }
        public float LayerDepth { get; set; } = 1.0f;

        public MapTile() {}
    }
}
