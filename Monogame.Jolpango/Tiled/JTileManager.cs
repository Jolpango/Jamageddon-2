using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Graphics.Sprites;
using MonoGame.Jolpango.Utilities;

namespace MonoGame.Jolpango.Tiled
{
    public class JTileManager : JComponent, IJInjectable<ContentManager>
    {
        private MapData map;
        ContentManager content;
        private JSpriteSheet spriteSheet;

        public JTileManager() { }

        public void Inject(ContentManager service)
        {
            if (service is null)
                throw new ArgumentNullException(nameof(service));
            content = service;
        }

        public void LoadMap(string path)
        {
            map = JJsonLoader.LoadTiledMap(path);
            TileSet tileset = map.TileSets[0]; // assume a single tileset
            foreach (MapLayer layer in map.Layers)
                layer.LoadTiles(new Vector2(tileset.TileHeight, tileset.TileHeight));

            string tilesetPath = tileset.Image[..tileset.Image.LastIndexOf('.')];  // strips file extension
            spriteSheet = new JSpriteSheet(content.Load<Texture2D>(tilesetPath), tileset.TileWidth, tileset.TileHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (map != null)
            {
                foreach (MapLayer layer in map.Layers)
                    foreach (List<MapTile> col in layer.tiles)
                        foreach (MapTile tile in col)
                        {
                            spriteBatch.Draw(
                                spriteSheet.Texture,
                                tile.Position,
                                spriteSheet.GetRegion(tile.TileIndex).Region,
                                Color.White
                            );
                        }
            }
        }
    }
}