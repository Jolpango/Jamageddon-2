using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGame.Jolpango.Tiled
{
    public class MapLayer
    {
        public List<List<MapTile>> tiles;

        public List<int> Data { get; set; }
        public int Height { get; set; }
        public double Opacity { get; set; }
        public List<LayerProperty> Properties { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        private Vector2 tileSize { get; set; }
        public bool IsPlaceable { get; set; }

        public void ResolveProperties()
        {
            foreach (LayerProperty prop in Properties)
                if (prop.Name == "isPlaceable")
                    IsPlaceable = prop.Value;
        }

        public void LoadTiles(Vector2 tileSize)
        {
            this.tileSize = tileSize;
            tiles = new List<List<MapTile>>(Height);
            int i = 0;
            for (int y = 0; y < Height; y++)
            {
                List<MapTile> row = new List<MapTile>(Width);
                for (int x = 0; x < Width; x++)
                {
                    MapTile tile = new MapTile()
                    {
                        TileIndex = Data[i++] - 1, // -1 since Tiled is using 1-based numbering
                        Position = new Vector2(x * tileSize.X, y * tileSize.Y)
                    };
                    row.Add(tile);
                }
                tiles.Add(row);
            }
        }

        public MapTile getTileAt(Vector2 position)
        {
            int x = (int)(position.X / tileSize.X);
            int y = (int)(position.Y / tileSize.Y);
            if (x >= Width || y >= Height || x < 0 || y < 0)
                return null;

            return tiles[y][x];
        }
    }
}
