using Microsoft.Xna.Framework;

namespace MonoGame.Jolpango.Tiled
{
    public class MapTile
    {
        public static int Empty = 7; // @HARDCODED: magic index 8(-1) in tile sheet to signify missing tile

        public int TileIndex { get; set; }
        public Vector2 Position { get; set; }
        public float LayerDepth { get; set; } = 1.0f;

        public MapTile() {}
    }
}
