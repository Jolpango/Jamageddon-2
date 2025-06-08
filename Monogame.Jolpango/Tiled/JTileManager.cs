using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Graphics.Sprites;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace MonoGame.Jolpango.Tiled
{
    public class JTileManager : JComponent, IJInjectable<ContentManager>
    {
        private MapData map;
        ContentManager content;
        private JSpriteSheet spriteSheet;
        public List<Vector2> path { get; private set; }

        public void Inject(ContentManager service)
        {
            if (service is null)
                throw new ArgumentNullException(nameof(service));
            content = service;
        }

        public void LoadMap(string mapPath)
        {
            string json = File.ReadAllText(mapPath);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new LayerConverter());
            map = JsonConvert.DeserializeObject<MapData>(json, settings);

            map.TileLayers = map.Layers.OfType<TileLayer>().ToList();
            map.ObjectLayers = map.Layers.OfType<ObjectLayer>().ToList();

            List<MapObject> lineObjects = map.ObjectLayers[0].Objects; // assume one object layer
            path = new List<Vector2>(lineObjects.Count);
            foreach (MapObject obj in lineObjects)
                path.Add(new Vector2((float)obj.X, (float)obj.Y));

            TileSet tileset = map.TileSets[0]; // assume one tileset
            foreach (TileLayer layer in map.TileLayers)
            {
                layer.ResolveCustomProperties();
                layer.LoadTiles(new Vector2(tileset.TileHeight, tileset.TileHeight));
            }

            string tilesetPath = tileset.Image[..tileset.Image.LastIndexOf('.')];  // strips file extension
            spriteSheet = new JSpriteSheet(content.Load<Texture2D>(tilesetPath), tileset.TileWidth, tileset.TileHeight);
        }

        public bool TileIsFree(JColliderComponent colliderComponent)
        {
            // TODO(anton): make this work regardloss of collider
            if (colliderComponent is not JBoxColliderComponent boxCollider)
                return false;

            Rectangle boundingBox = boxCollider.BoundingBox;
            foreach (TileLayer layer in map.TileLayers)
            {
                if (layer.IsPlaceable)
                {
                    MapTile topLeftTile = layer.getTileAt(new Vector2(boundingBox.Left, boundingBox.Top));
                    MapTile topRightTile = layer.getTileAt(new Vector2(boundingBox.Right, boundingBox.Top));
                    MapTile bottomLeftTile = layer.getTileAt(new Vector2(boundingBox.Left, boundingBox.Bottom));
                    MapTile bottomRightTile = layer.getTileAt(new Vector2(boundingBox.Right, boundingBox.Bottom));

                    if (topLeftTile == null || topLeftTile.isEmpty ||
                        topRightTile == null || topRightTile.isEmpty ||
                        bottomLeftTile == null || bottomLeftTile.isEmpty ||
                        bottomRightTile == null || bottomRightTile.isEmpty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (map is null)
                return;

            foreach (TileLayer layer in map.TileLayers)
                foreach (List<MapTile> col in layer.tiles)
                    foreach (MapTile tile in col)
                    {
                        if (tile.isEmpty)
                            continue;

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