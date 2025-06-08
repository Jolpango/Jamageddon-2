using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Jolpango.Utilities
{
    public static class JFontCache
    {
        public static SpriteFont DefaultFont { get; private set; }
        public static void Initialize(SpriteFont defaultFont)
        {
            DefaultFont = defaultFont;
        }
    }
}
