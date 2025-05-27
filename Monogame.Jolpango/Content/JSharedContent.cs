using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Graphics.Sprites;
using MonoGame.Jolpango.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.Content
{
    public class JSharedContent
    {
        private static JSharedContent instance;
        public static JSharedContent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JSharedContent();
                }
                return instance;
            }
        }
        private Dictionary<string, JAnimationSettings> animationSettings;
        private JSharedContent()
        {
            animationSettings = new Dictionary<string, JAnimationSettings>();
        }
        private Game game;
        public Game Game { get => game; private set => game = value; }
        public void Initialize(Game game)
        {
            this.game = game;
        }
        public JAnimationSettings GetAnimation(string name)
        {
            // try and get animation
            animationSettings.TryGetValue(name, out var settings);
            if (settings is null)
            {
                settings = JJsonLoader.ReadAnimationFromFile(name);
            }
            return settings;
        }
    }
}
