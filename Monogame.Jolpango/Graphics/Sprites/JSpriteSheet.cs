using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Sprites
{
    public class JSpriteSheet
    {
        public Texture2D Texture { get; set; }
        private JTextureRegion[] jTextureRegions;
        public int RegionWidth { get; set; }
        public int RegionHeight { get; set; }
        public JSpriteSheet(Texture2D texture, int regionWidth, int regionHeight)
        {
            Texture = texture;
            RegionWidth = regionWidth;
            RegionHeight = regionHeight;
            int tilesWide = texture.Width / regionWidth;
            int tilesHigh = texture.Height / regionHeight;
            int numberOfTiles = tilesWide * tilesHigh;
            jTextureRegions = new JTextureRegion[numberOfTiles];
            for(int i = 0; i < numberOfTiles; i++)
            {
                jTextureRegions[i] = new JTextureRegion(texture, regionWidth, regionHeight, i);
            }
        }
        public JTextureRegion GetRegion(int index)
        {
            return jTextureRegions[index];
        }
    }
}
