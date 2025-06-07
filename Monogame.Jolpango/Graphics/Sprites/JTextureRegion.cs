using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Jolpango.Graphics.Sprites
{
    public class JTextureRegion
    {
        public Rectangle Region { get; set; }
        public JTextureRegion(Texture2D texture, int width, int height, int index)
        {
            int tilesPerRow = texture.Width / width;
            int xPos = (index % tilesPerRow) * width;
            int yPos = (index / tilesPerRow) * height;

            Region = new Rectangle(xPos, yPos, width, height);
        }
    }
}
