using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Jolpango.Utilities
{
    public static class JTextureCache
    {
        public static Texture2D White;

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            White = new Texture2D(graphicsDevice, 1, 1);
            White.SetData(new[] { Color.White });
        }
    }
}
