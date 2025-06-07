using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.Jolpango.ECS
{
    public class JSceneManager
    {
        private Stack<JGameScene> gameScenes;
        public JGameScene CurrentScene
        {
            get
            {
                return gameScenes.Peek();
            }
        }
        public JSceneManager()
        {
            gameScenes = new Stack<JGameScene>();
        }

        public JGameScene AddScene(JGameScene scene, bool pop = false)
        {
            JGameScene sceneToReturn = null;
            scene.LoadContent();
            gameScenes.Push(scene);
            if (pop)
                sceneToReturn = gameScenes.Pop();
            return sceneToReturn;
        }
        public JGameScene PopScene()
        {
            return gameScenes.Pop();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene?.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene?.Draw(spriteBatch);
        }
    }
}
