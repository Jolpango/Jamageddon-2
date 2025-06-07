using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Graphics.Sprites
{
    public class JAnimationSettings
    {
        public JTextureAtlasSettings TextureAtlas { get; set; }
        public Dictionary<string, JAnimationCycleSettings> Cycles { get; set; }
    }

    public class JTextureAtlasSettings
    {
        public string Texture { get; set; }
        public int RegionWidth { get; set; }
        public int RegionHeight { get; set; }
    }

    public class JAnimationCycleSettings
    {
        public int[] Frames { get; set; }
        public float FrameDuration { get; set; }
    }
}
